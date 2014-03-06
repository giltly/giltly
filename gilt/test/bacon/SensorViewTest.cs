using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy;
using Nancy.Testing;
using System.Xml;
using System.Xml.Linq;
using gilt.test.auth;
using gilt.bacon.modules;

namespace gilt.test
{
    [TestClass]
    public class SensorViewTest : TestBase
    {
        [ClassInitialize]
        public new static void ClassInitialize(TestContext testContext)
        {
            TestBase.ClassInitialize(testContext);
        }

        protected override void BeforeTestMethod()
        {
            this.AuthenticateBrowser();
        }

        [TestCategory("View"), TestCategory("Sensor")]
        [TestMethod]
        public void sensor_list()
        {
            var response = _browser.Get(GiltlyRoutes.SENSOR_LIST, (with) =>
            {
                with.HttpRequest();
            });
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }
    }
}
