using gilt.dblinq.events;
using gilt.dblinq.proxy;

namespace gilt.bacon.modules.login
{
    public partial class AdminModule : ModuleBase<IEventRepository, EventProxy>
    {
        /// <summary>
        /// Admin HTML Responses
        /// </summary>
        protected override void HtmlResponses()
        {
            Get[GiltlyRoutes.ADMIN_EVENT_DELETE] = x =>
            {
                return this.RenderView("EventDelete");
            };
            Get[GiltlyRoutes.ADMIN_GEO_DELETE] = x =>
            {
                return this.RenderView("GeoDelete");
            };
            Get[GiltlyRoutes.ADMIN_SNORT_DELETE] = x =>
            {
                return this.RenderView("SnortDelete");
            };
            Get[GiltlyRoutes.ADMIN_LOG_DELETE] = x =>
            {
                return this.RenderView("LogDelete");
            };
            Get[GiltlyRoutes.ADMIN_DATA_RESET] = x =>
            {
                return this.RenderView("ResetData");
            };
        }
    }
}
