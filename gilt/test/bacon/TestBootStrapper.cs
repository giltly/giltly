using gilt.bacon.auth;
using gilt.bacon.exceptions;
using gilt.bacon.modules;
using gilt.dblinq.events;
using gilt.dblinq.ipdata;
using gilt.dblinq.logs;
using gilt.dblinq.roles;
using gilt.dblinq.search;
using gilt.dblinq.sensor;
using gilt.dblinq.signatures;
using gilt.dblinq.user;
using gilt.test.auth;
using gilt.test.moq;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace gilt.test
{
    public class TestBootStrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            StaticConfiguration.EnableRequestTracing = true;
            StaticConfiguration.Caching.EnableRuntimeViewUpdates = true;
            StaticConfiguration.Caching.EnableRuntimeViewDiscovery = true;
            Nancy.Json.JsonSettings.MaxJsonLength = 5000000;

            pipelines.OnError += ((ctx, exception) =>
            {
                if (exception is InvalidPagingParameterException)
                {
                    return new Nancy.Response
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        ContentType = "text/html"
                    };
                }
                return HttpStatusCode.InternalServerError;
            }
            );
        }

        #region Test File Location override RootPathProvider
        protected override IRootPathProvider RootPathProvider
        {
            get { return new TestingRootPathProvider(); }
        }
        #endregion

        #region Nancy Forms Authentication
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            //specificy how the IOC container will resolve our repositories

            //these moqs do not hit the database as all -- perfect for testing

            //events
            container.Register<IEventRepository, MoqEventRepository>().AsMultiInstance();
            container.Register<IEventCommentsRepository, MoqEventCommentRepository>().AsMultiInstance();
            
            //sensor
            container.Register<ISensorRepository, MoqSensorRepository>().AsMultiInstance();

            //search
            container.Register<ISearchRepository, MoqSearchRepository>().AsMultiInstance();

            //roles
            container.Register<IRolesRepository, MoqRolesRepository>().AsMultiInstance();
            container.Register<IUserRolesRepository, MoqUserRolesRepository>().AsMultiInstance();

            //ipdata
            container.Register<IDatasRepository, MoqDataRepository>().AsMultiInstance();
            container.Register<IIcmpHeadersRepository, MoqIcmpHeaderRepository>().AsMultiInstance();
            container.Register<ITcpHeadersRepository, MoqTcpHeaderRepository>().AsMultiInstance();
            container.Register<IUdpHeadersRepository, MoqUdpHeaderRepository>().AsMultiInstance();
                        
            //signature
            container.Register<ISignatureRepository, MoqSignatureRepository>().AsMultiInstance();

            //user
            container.Register<IUsersRepository, MoqUserRepository>().AsMultiInstance();
            container.Register<IUserGroupsRepository, MoqUserGroupRepository>().AsMultiInstance();
            container.Register<IUserUserGroupsRepository, MoqUserUserGroupRepository>().AsMultiInstance();            
            
            //logs
            container.Register<ILogHistoryRepository, MoqLogHistoryRepository>().AsMultiInstance();
            container.Register<ILogEntryRepository, MoqLogEntryRepository>().AsMultiInstance();

            //
            // Use a fake login for testing
            // 
            container.Register<IDatabaseUserMapping, TestUserDatabase>().AsMultiInstance();
        }

        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);

            // Here we register our user mapper as a per-request singleton.
            // As this is now per-request we could inject a request scoped
            // database "context" or other request scoped services.
            container.Register<IUserMapper, TestUserDatabase>();
        }

        protected override void RequestStartup(TinyIoCContainer requestContainer, IPipelines pipelines, NancyContext context)
        {
            // At request startup we modify the request pipelines to
            // include forms authentication - passing in our now request
            // scoped user name mapper.
            //
            // The pipelines passed in here are specific to this request,
            // so we can add/remove/update items in them as we please.
            var formsAuthConfiguration =
                new FormsAuthenticationConfiguration()
                {
                    RedirectUrl = GiltlyRoutes.LOGIN,
                    UserMapper = requestContainer.Resolve<IUserMapper>(),
                };

            FormsAuthentication.Enable(pipelines, formsAuthConfiguration);
        }
        #endregion
    }
    
}
