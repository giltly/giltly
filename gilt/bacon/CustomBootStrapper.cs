using gilt.dblinq;
using gilt.dblinq.events;
using gilt.dblinq.search;
using gilt.dblinq.sensor;
using gilt.dblinq.roles;
using gilt.dblinq.user;
using gilt.bacon.auth;
using gilt.bacon.exceptions;
using gilt.bacon.modules;
using gilt.util;
using Nancy;
using NLog;
using Nancy.Conventions;
using Nancy.Diagnostics;
using Nancy.Authentication.Forms;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using System;
using System.Threading;
using System.IO;

namespace gilt.bacon
{
    public class CustomBootStrapper : DefaultNancyBootstrapper
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
                        StatusCode = HttpStatusCode.OK,
                        ContentType = "text/html"
                    };
                }
                return HttpStatusCode.InternalServerError;
            }
            );
        }

        protected override DiagnosticsConfiguration DiagnosticsConfiguration
        {
            get { return new DiagnosticsConfiguration { Password = @"password" }; }
        }

        #region Nancy Forms Authentication
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            //specificy how the IOC container will resolve our repositories
            container.Register<IEventRepository, EventRepository>().UsingConstructor(() => new giltdbDataContext(AppSettings.DatabaseConnectionString)).AsMultiInstance();
            container.Register<IEventCommentsRepository, EventCommentsRepository>().UsingConstructor(() => new giltdbDataContext(AppSettings.DatabaseConnectionString)).AsMultiInstance();            
            container.Register<IUsersRepository, UserRepository>().UsingConstructor(() => new giltdbDataContext(AppSettings.DatabaseConnectionString)).AsMultiInstance();
            container.Register<ISensorRepository, SensorRepository>().UsingConstructor(() => new giltdbDataContext(AppSettings.DatabaseConnectionString)).AsMultiInstance();
            container.Register<ISearchRepository, SearchRepository>().UsingConstructor(() => new giltdbDataContext(AppSettings.DatabaseConnectionString)).AsMultiInstance();
            container.Register<IRolesRepository, RolesRepository>().UsingConstructor(() => new giltdbDataContext(AppSettings.DatabaseConnectionString)).AsMultiInstance();
            container.Register<IUserRolesRepository, UserRolesRepository>().UsingConstructor(() => new giltdbDataContext(AppSettings.DatabaseConnectionString)).AsMultiInstance();
            container.Register<IUserGroupsRepository, UserGroupsRepository>().UsingConstructor(() => new giltdbDataContext(AppSettings.DatabaseConnectionString)).AsMultiInstance();
            container.Register<IUserUserGroupsRepository, UserUserGroupsRepository>().UsingConstructor(() => new giltdbDataContext(AppSettings.DatabaseConnectionString)).AsMultiInstance();
            container.Register<IDatabaseUserMapping, GiltyUserDatabase>().AsMultiInstance();
        }

        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);

            // Here we register our user mapper as a per-request singleton.
            // As this is now per-request we could inject a request scoped
            // database "context" or other request scoped services.
            container.Register<IUserMapper, GiltyUserDatabase>();
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

            //before starting the request use the built in nancy context handling
            //http://www.jefclaes.be/2013/04/internationalization-in-nancyfx.html
            //and set the current culture
            //
            // change your language preference in the browser and BAM -- different language
            //
            pipelines.BeforeRequest += ctx =>
            {
                Thread.CurrentThread.CurrentCulture = ctx.Culture;
                return null;
            };
         
            //log all requests to the database
            LogManager.GetLogger("database").Trace(String.Format("{0} ** {1} ** {2}", 
                context.Request.UserHostAddress, 
                context.Request.Url.ToString(), 
                context.Request.Query.ToString() ));
        }
        #endregion
    }

}
