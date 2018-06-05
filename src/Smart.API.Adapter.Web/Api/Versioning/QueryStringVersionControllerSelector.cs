using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;

namespace Smart.API.Adapter.Web.Api {
	/// <summary>
	/// QueryStringVersionControllerSelector
	/// </summary>
	public class QueryStringVersionControllerSelector : IHttpControllerSelector {
		/// <summary>
		/// ControllerSuffix
		/// </summary>
		public static readonly string ControllerSuffix = "Controller";

		private const string VersionKey = "v";
		private const string ControllerKey = "controller";

		private readonly HttpConfiguration _configuration;
		private readonly HashSet<string> _duplicateControllers;
		private readonly Lazy<ConcurrentDictionary<string, HttpControllerDescriptor>> _controllerInfoCache;
		private readonly Func<HttpRequestMessage, string> _namespaceResolver;

		/// <summary>
		/// Initializes a new instance of the <see cref="QueryStringVersionControllerSelector"/> class.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		/// <param name="namespaceResolver">The Namespace Resolver Func.</param>
		public QueryStringVersionControllerSelector(HttpConfiguration configuration, Func<HttpRequestMessage, string> namespaceResolver) {
			if(configuration == null) {
				throw new ArgumentNullException("configuration");
			}

			if(namespaceResolver == null) {
				throw new ArgumentNullException("namespaceResolver");
			}

			_configuration = configuration;
			_namespaceResolver = namespaceResolver;
			_duplicateControllers = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			_controllerInfoCache = new Lazy<ConcurrentDictionary<string, HttpControllerDescriptor>>(InitializeControllerInfoCache);
		}

		private ConcurrentDictionary<string, HttpControllerDescriptor> InitializeControllerInfoCache() {
			var result = new ConcurrentDictionary<string, HttpControllerDescriptor>(StringComparer.OrdinalIgnoreCase);

			// Create a lookup table where key is "version.controller". The value of "version" is the last
			// segment of the full namespace. For example:
			// MyApplication.Controllers.V1.ProductsController => "V1.Products"
			IAssembliesResolver assembliesResolver = _configuration.Services.GetAssembliesResolver();
			IHttpControllerTypeResolver controllersResolver = _configuration.Services.GetHttpControllerTypeResolver();

			ICollection<Type> controllerTypes = controllersResolver.GetControllerTypes(assembliesResolver);

			foreach(Type controllerType in controllerTypes) {
				var segments = controllerType.Namespace.Split(Type.Delimiter);

				// For the dictionary key, strip "Controller" from the end of the type name.
				// This matches the behavior of DefaultHttpControllerSelector.
				var controllerName = controllerType.Name.Remove(controllerType.Name.Length - ControllerSuffix.Length);

				var key = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", segments[segments.Length - 1], controllerName);

				// Check for duplicate keys.
				if(result.Keys.Contains(key)) {
					_duplicateControllers.Add(key);
				}
				else {
					result.TryAdd(key, new HttpControllerDescriptor(_configuration, controllerName, controllerType));
				}
			}

			// Remove any duplicates from the dictionary, because these create ambiguous matches. 
			// For example, "Foo.V1.ProductsController" and "Bar.V1.ProductsController" both map to "v1.products".
			foreach(string duplicateController in _duplicateControllers) {
				HttpControllerDescriptor descriptor;
				result.TryRemove(duplicateController, out descriptor);
			}

			return result;
		}

		/// <summary>
		/// Get a value from the route data, if present.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="routeData"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		private static T GetRouteVariable<T>(IHttpRouteData routeData, string name) {
			object result = null;
			if(routeData.Values.TryGetValue(name, out result)) {
				return (T)result;
			}
			return default(T);
		}

		/// <summary>
		/// Selects a <see cref="System.Web.Http.Controllers.HttpControllerDescriptor"/> for the given <see cref="System.Net.Http.HttpRequestMessage"/>.
		/// </summary>
		/// <param name="request">The HTTP request message.</param>
		/// <returns>The <see cref="HttpControllerDescriptor"/> instance for the given <see cref="HttpRequestMessage"/>.</returns>
		public virtual HttpControllerDescriptor SelectController(HttpRequestMessage request) {
			IHttpRouteData routeData = request.GetRouteData();
			if(routeData == null) {
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}

			// Get the namespaceName variables from the http request message.
			string namespaceName = _namespaceResolver(request);
			if(namespaceName == null) {
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}

			// Get the controller variables from the route data.
			string controllerName = GetRouteVariable<string>(routeData, ControllerKey);
			if(controllerName == null) {
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}

			// Find a matching controller.
			string key = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", namespaceName, controllerName);

			HttpControllerDescriptor controllerDescriptor;
			if(_controllerInfoCache.Value.TryGetValue(key, out controllerDescriptor)) {
				return controllerDescriptor;
			}
			else if(_duplicateControllers.Contains(key)) {
				throw new HttpResponseException(
					request.CreateErrorResponse(HttpStatusCode.InternalServerError,
					"Multiple controllers were found that match this request."));
			}
			else {
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}
		}

		/// <summary>
		/// Returns a map, keyed by controller string, of all <see cref="HttpControllerDescriptor"/> that the selector can select.
		/// </summary>
		/// <returns></returns>
		public virtual IDictionary<string, HttpControllerDescriptor> GetControllerMapping() {
			return _controllerInfoCache.Value;
		}
	}
}
