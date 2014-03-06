using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy;
using Nancy.Testing;
using System.Xml;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using gilt.test.auth;
using gilt.bacon.modules;

namespace gilt.test
{
    [TestClass]
    public class SearchViewsTest : TestBase
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

        [TestCategory("View"), TestCategory("Search")]
        [TestMethod]
        public void search_add_new_view()
        {
            var response = _browser.Get(GiltlyRoutes.SEARCH_ADD_VIEW, (with) =>
            {
                with.HttpRequest();
            });            
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);            
        }

        [TestCategory("Search")]
        [TestMethod]
        public void search_update()
        {
            var response = _browser.Post(GiltlyRoutes.SEARCH_UPDATE, (with) =>
            {
                with.HttpRequest();
                with.FormValue("Id", "1");                
            });
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [TestCategory("Search")]
        [TestMethod]
        public void search_add()
        {
            var response = _browser.Post(GiltlyRoutes.SEARCH_UPDATE, (with) =>
            {
                //without sending Id this is an update
                with.HttpRequest();
                with.FormValue("Name", "Test Search 1");  
            });            
            Assert.AreEqual(response.Cookies.ElementAt(0).Value, "Successfully Added");             
        }

    }
}
