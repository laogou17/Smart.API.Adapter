using System;

namespace Smart.API.Adapter.Web.Api {

	/// <summary>
	/// Specifies a response code for the web api when errored.
	/// </summary>
	public static class Contract {
		/// <summary>
		/// Specifies a precondition contract for the request argument. 
		/// Response HTTP StatusCode will be 400.
		/// </summary>
		/// <param name="condition">The conditional expression to test.</param>
		/// <param name="errorMessage">The message to display if the condition is false.</param>
		public static void Requires(Func<bool> condition, string errorMessage) {
			Requires(condition, errorMessage, ArgumentException.DefaultCode);
		}

		/// <summary>
		/// Specifies a precondition contract for the request argument. 
		/// Response HTTP StatusCode will be 400.
		/// </summary>
		/// <param name="condition">The conditional expression to test.</param>
		/// <param name="errorMessage">The message to display if the condition is false.</param>
		/// <param name="errorCode">The code to display if the condition is false.</param>
		public static void Requires(Func<bool> condition, string errorMessage, string errorCode) {
			Requires(condition(), errorMessage, errorCode);
		}

		/// <summary>
		/// Specifies a precondition contract for the request argument. 
		/// Response HTTP StatusCode will be 400.
		/// </summary>
		/// <param name="condition">The conditional expression to test.</param>
		/// <param name="errorMessage">The message to display if the condition is false.</param>
		public static void Requires(bool condition, string errorMessage) {
			Requires(condition, errorMessage, ArgumentException.DefaultCode);
		}

		/// <summary>
		/// Specifies a precondition contract for the request argument. 
		/// Response HTTP StatusCode will be 400.
		/// </summary>
		/// <param name="condition">The conditional expression to test.</param>
		/// <param name="errorMessage">The message to display if the condition is false.</param>
		/// <param name="errorCode">The code to display if the condition is false.</param>
		public static void Requires(bool condition, string errorMessage, string errorCode) {
			if(condition == false) {
				Error.ThrowApiArgumentException(errorMessage, errorCode);
			}
		}

		/// <summary>
		/// Specifies a postcondition contract for a provided exit condition and a message to display if the condition is false.
		/// Response HTTP StatusCode will be 400.
		/// </summary>
		/// <param name="condition">The conditional expression to test.</param>
		/// <param name="errorMessage">The message to display if the condition is false.</param>
		/// <param name="errorCode">The code to display if the condition is false.</param>
		public static void Ensures(bool condition, string errorMessage, string errorCode) {
			if(condition == false) {
				Error.ThrowApiException(errorMessage, errorCode);
			}
		}
	}

	public static class Error {
		public static void ThrowArgumentException(string errorMessage) {
			throw new System.ArgumentException(errorMessage);
		}
		public static void ThrowArgumentException(string errorMessage, Exception innerException) {
			throw new System.ArgumentException(errorMessage, innerException);
		}
		public static void ThrowArgumentException(string errorMessage, string paramName) {
			throw new System.ArgumentException(errorMessage, paramName);
		}
		public static void ThrowArgumentException(string errorMessage, string paramName, Exception innerException) {
			throw new System.ArgumentException(errorMessage, paramName, innerException);
		}
		public static void ThrowArgumentNullException(string paramName) {
			throw new System.ArgumentNullException(paramName);
		}
		public static void ThrowArgumentNullException(string errorMessage, string paramName) {
			throw new System.ArgumentNullException(paramName, errorMessage);
		}
		/// <summary>
		/// Throw an Smart.API.Adapter.Web.Api.ApiException.
		/// Response HTTP StatusCode will be 400.
		/// </summary>
		/// <param name="errorMessage">The message to display if the request failure.</param>
		/// <param name="errorCode">The code to display if the request failure.</param>
		public static void ThrowApiException(string errorMessage, string errorCode) {
			throw new ApiException(errorMessage, errorCode);
		}
		public static void ThrowApiArgumentException(string errorMessage) {
			throw new ArgumentException(errorMessage);
		}
		public static void ThrowApiArgumentException(string errorMessage, string errorCode) {
			throw new ArgumentException(errorMessage, errorCode);
		}
		/// <summary>
		/// Throw an Smart.API.Adapter.Web.Api.InvalidSignException.
		/// Response HTTP StatusCode will be 401.
		/// </summary>
		/// <param name="errorMessage">The message to display if verify sign failure.</param>
		/// <param name="errorCode">The code to display if verify sign failure.</param>
		public static void ThrowInvalidSignException(string errorMessage, string errorCode) {
			throw new InvalidSignException(errorMessage, errorCode);
		}
		/// <summary>
		/// Throw an Smart.API.Adapter.Web.Api.ThrowNoAccessRightException.
		/// Response HTTP StatusCode will be 401.
		/// </summary>
		public static void ThrowNoAccessRightException() {
			throw new NoAccessRightException();
		}
		/// <summary>
		/// Throw an Smart.API.Adapter.Web.Api.ThrowNoAccessRightException.
		/// Response HTTP StatusCode will be 401.
		/// </summary>
		/// <param name="errorMessage">The message to display if no rights to access.</param>
		/// <param name="errorCode">The code to display if no rights to access.</param>
		public static void ThrowNoAccessRightException(string errorMessage, string errorCode) {
			throw new NoAccessRightException(errorMessage, errorCode);
		}
		/// <summary>
		/// Throw an Smart.API.Adapter.Web.Api.AccessFrequencyException.
		/// Response HTTP StatusCode will be 403.
		/// </summary>
		public static void ThrowAccessFrequencyException() {
			throw new AccessFrequencyException();
		}
	}
}
