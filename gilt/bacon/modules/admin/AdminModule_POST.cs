using gilt.dblinq.events;
using gilt.dblinq.proxy;
using Nancy.Extensions;
using System;

namespace gilt.bacon.modules.login
{
    public partial class AdminModule : ModuleBase<IEventRepository, EventProxy>
    {
        /// <summary>
        /// Admin POST Handlers
        /// </summary>
        protected override void PostResponses()
        {
            Post[GiltlyRoutes.ADMIN_EVENT_DELETEEVENTS] = x =>
            {
                _repository.DeleteAllEventData();
                return this.Context.GetRedirect(String.Format("{0}", GiltlyRoutes.INDEX));
            };
            Post[GiltlyRoutes.ADMIN_GEO_DELETEGEO] = x =>
            {
                _repository.DeleteAllGeoData();
                return this.Context.GetRedirect(String.Format("{0}", GiltlyRoutes.INDEX));
            };
            Post[GiltlyRoutes.ADMIN_SNORT_DELETESNORT] = x =>
            {
                _repository.DeleteAllGeoData();
                return this.Context.GetRedirect(String.Format("{0}", GiltlyRoutes.INDEX));
            };
            Post[GiltlyRoutes.ADMIN_LOG_DELETELOG] = x =>
            {
                _repository.DeleteAllLogHistoryData();
                return this.Context.GetRedirect(String.Format("{0}", GiltlyRoutes.INDEX));
            };
            Post[GiltlyRoutes.ADMIN_DATA_DELETEEVENTS] = x =>
            {
                _repository.DeleteAllNonGeoData();
                return this.Context.GetRedirect(String.Format("{0}", GiltlyRoutes.INDEX));
            };
        }
    }
}
