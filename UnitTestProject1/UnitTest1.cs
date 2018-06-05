using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace UnitTestProject1
{
    [TestClass]

    public class UnitTest1
    {
        [TestMethod]

        public void TestMethod1()
        {
            //var timestamp = Smart.API.Adapter.Common.StringHelper.ConvertDateTimeInt(DateTime.Now);
            //Assert.IsNotNull(timestamp);

            Smart.API.Adapter.Common.LogHelper.Info("12321456");
        }
    }
}
