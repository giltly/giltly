using System;
using System.Data.Linq;
using System.Collections.Generic;
using System.Linq;
using gilt.dblinq.proxy;

namespace gilt.dblinq.sensor
{
    /// <summary>
    /// Sensor Repository
    /// </summary>
    public sealed class SensorRepository : GenericRepository<SensorProxy>, ISensorRepository
    {
        /// <summary>
        /// Initialize the Senor Query
        /// </summary>
        protected override void InitializeQuery()
        {
            _genericQuery = (from u in DataContext.Sensors select new SensorProxy(u) 
                { 
                    EventCount = (from e in DataContext.Events where e.SensorId == u.Id select e).Count()  ,
                    DetailString = (from d in DataContext.Details where d.Id == u.DetailId select d).Single().Text,
                    EncodingString = (from en in DataContext.Encodings where en.Id == u.EncodingId select en).Single().Text,
                } );
        }
    }
}
