using gilt.dblinq.proxy;
using gilt.dblinq.sensor;
using Nancy;
using System;
using System.Linq;

namespace gilt.bacon.modules.sensor
{
    public partial class SensorModule : ModuleBase<ISensorRepository, SensorProxy>
    {
        protected override void JsonResponses()
        {
            Get[GiltlyRoutes.SENSOR_PAGED] = parameters =>
            {
                PagingParameters pp = new PagingParameters(parameters);
                int pageSize = pp.PageSize;
                int page = pp.PageNumber;

                SortParameters sp = new SortParameters(this.Request.Query);

                int eventCount = _repository.GetCount();
                int totalPages = (int)Math.Ceiling((float)eventCount / (float)pageSize);
                var rows = _repository.FindBy(a => 1 == 1).AsQueryable().Skip(page * pageSize).Take(pageSize);
                return Response.AsJson(
                    new
                    {
                        Rows = rows,
                        NumberPages = totalPages
                    });
            };

            Get[GiltlyRoutes.SENSOR_BY_ID] = parameters =>
            {
                decimal id = parameters.Id;
                return Response.AsJson(new object[] { _repository.FindBy(a => a.Id == id) });
            };
        } 
    }
}
