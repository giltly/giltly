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
    public class EventViewsTest : TestBase
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

        #region ListMethods
        [TestCategory("View"), TestCategory("Event")]
        [TestMethod]
        public void events_list()
        {            
            var response = _browser.Get(GiltlyRoutes.EVENT_LIST, (with) =>
            {
                with.HttpRequest();
            });            
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);            
        }

        [TestCategory("View"), TestCategory("Event")]
        [TestMethod]
        public void events_by_country_list()
        {            
            var response = _browser.Get(GiltlyRoutes.EVENT_BY_COUNTRY_LIST, (with) =>
            {
                with.HttpRequest();
            });            
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [TestCategory("View"), TestCategory("Event")]
        [TestMethod]
        public void events_by_destination_port_list()
        {
            var response = _browser.Get(GiltlyRoutes.EVENT_BY_DESTINATIONPORT_LIST, (with) =>
            {
                with.HttpRequest();
            });
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [TestCategory("View"), TestCategory("Event")]
        [TestMethod]
        public void events_by_source_port_list()
        {
            var response = _browser.Get(GiltlyRoutes.EVENT_BY_SOURCEPORT_LIST, (with) =>
            {
                with.HttpRequest();
            });
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [TestCategory("View"), TestCategory("Event")]
        [TestMethod]
        public void events_by_ip_list()
        {            
            var response = _browser.Get(GiltlyRoutes.EVENT_BY_IP_LIST, (with) =>
            {
                with.HttpRequest();
            });
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }
        #endregion

        [TestCategory("Event")]
        [TestMethod]
        public void event_by_id()
        {
            var response = _browser.Get(GiltlyRoutes.EVENT_BY_ID.Replace("(?<Id>[0-9]*)","/1"), (with) =>
            {
                with.HttpRequest();
            });
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [TestCategory("Event")]
        [TestMethod]
        public void event_by_id_bad()
        {
            var response = _browser.Get(GiltlyRoutes.EVENT_BY_ID.Replace("(?<Id>[0-9]*)", "/ABC"), (with) =>
            {
                with.HttpRequest();
            });
            Assert.AreEqual(response.StatusCode, HttpStatusCode.InternalServerError);
        }

        [TestCategory("Event")]
        [TestMethod]
        public void event_previous()
        {
            var response = _browser.Get(GiltlyRoutes.EVENT_PREVIOUS.Replace("(?<MinutesBackStart>[0-9]*)/(?<MinutesDuration>[0-9]*)", "/10/1"), (with) =>
            {
                with.HttpRequest();
            });
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [TestCategory("Event")]
        [TestMethod]
        public void event_previous_bad_one_arg()
        {
            var response = _browser.Get(GiltlyRoutes.EVENT_PREVIOUS.Replace("(?<MinutesBackStart>[0-9]*)/(?<MinutesDuration>[0-9]*)", "/10"), (with) =>
            {
                with.HttpRequest();
            });
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
        }

        [TestCategory("Event")]
        [TestMethod]
        public void event_previous_bad()
        {
            var response = _browser.Get(GiltlyRoutes.EVENT_PREVIOUS.Replace("(?<MinutesBackStart>[0-9]*)/(?<MinutesDuration>[0-9]*)", "/abc"), (with) =>
            {
                with.HttpRequest();
            });
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
        }
    }
}
