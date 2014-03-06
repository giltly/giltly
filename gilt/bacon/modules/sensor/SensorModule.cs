using gilt.dblinq.proxy;
using gilt.dblinq.search;
using gilt.dblinq.sensor;
using Nancy.Security;

namespace gilt.bacon.modules.sensor
{
    public partial class SensorModule : ModuleBase<ISensorRepository, SensorProxy>
    {
        public SensorModule(ISensorRepository sensorRepository, ISearchRepository searchRepository)
            : base(sensorRepository, searchRepository)
        {
            this.RequiresAuthentication();
        }
    }
}
