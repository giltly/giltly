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
    public class UserTest : TestBase
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

        [TestMethod]
        public void user_profile()
        {
            var response = _browser.Get(GiltlyRoutes.USER_PROFILE, (with) =>
            {
                with.HttpRequest();
            });
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }
    }
}
