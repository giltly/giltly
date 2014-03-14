using gilt.dblinq.events;
using gilt.dblinq.proxy;

namespace gilt.bacon.modules.events
{
    public partial class EventModule : ModuleBase<IEventRepository, EventProxy>
    {
        protected override void HtmlResponses()
        {
            Get[GiltlyRoutes.EVENT_LIST] = parameters =>
            {
                return this.RenderView("EventList");
            };

            Get[GiltlyRoutes.EVENT_BY_IP_LIST] = parameters =>
            {
                return this.RenderView("EventByIpList");
            };

            Get[GiltlyRoutes.EVENT_BY_COUNTRY_LIST] = parameters =>
            {
                return this.RenderView("EventByCountryList");
            };

            Get[GiltlyRoutes.EVENT_BY_DESTINATIONPORT_LIST] = parameters =>
            {
                return this.RenderView("EventByDestinationPortList");
            };

            Get[GiltlyRoutes.EVENT_BY_SOURCEPORT_LIST] = parameters =>
            {
                return this.RenderView("EventBySourcePortList");
            };

            Get[GiltlyRoutes.EVENT_TIMELINE] = parameters =>
            {
                return this.RenderView("EventTimeLine");
            };

            Get[GiltlyRoutes.EVENT_VIEW_BY_ID] = parameters =>
            {
                decimal id = parameters.Id;
                return this.RenderView("EventViewById");
            };
        }
    }
}
