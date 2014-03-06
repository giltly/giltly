using gilt.dblinq.proxy;
using gilt.dblinq.sensor;

namespace gilt.bacon.modules.sensor
{
    public partial class SensorModule : ModuleBase<ISensorRepository, SensorProxy>
    {
        protected override void HtmlResponses()
        {
            Get[GiltlyRoutes.SENSOR_LIST] = parameters =>
            {
                return this.RenderView("SensorList");
            };
        } 
    }
}
