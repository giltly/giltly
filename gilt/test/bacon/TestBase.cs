using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy.Testing;
using gilt.bacon.modules;
using gilt.test.auth;

namespace gilt.test
{
    [TestClass]
    public class TestBase
    {
        protected static TestContext _testContext;
        protected static Browser _browser;
        protected static TestBootStrapper _bootStrapper;

        public TestContext TestContext
        {
            get { return _testContext; }
            set { _testContext = value; }
        }

        protected virtual void AuthenticateBrowser()
        {
            _browser = _browser.Post(GiltlyRoutes.LOGIN, (with) =>
            {
                with.HttpRequest();
                with.FormValue("Username", TestUserDatabase.USERNAME);
                with.FormValue("Password", TestUserDatabase.PASSWORD);
                with.FormValue("RememberMe", "1");
            }).Then;
        }

        protected virtual void BeforeTestMethod()
        {
        }

        protected virtual void AfterTestMethod()
        {
        }

        protected virtual void AfterTestClass()
        {
        }

        /// <summary>
        /// Runs before each test method
        /// </summary>
        [TestInitialize]
        public void TestMethodInitialize()
        {
            _browser = new Browser(_bootStrapper);            
            BeforeTestMethod();
            
        }

        /// <summary>
        /// Runs before first test in class
        /// </summary>
        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            _bootStrapper = new TestBootStrapper();
            _testContext = testContext;
        }

        /// <summary>
        /// Runs after each test method
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            AfterTestClass();
        }
    }
}
