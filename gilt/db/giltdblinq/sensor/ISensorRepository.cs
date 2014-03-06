using System;
using System.Collections.Generic;
using gilt.dblinq.proxy;

namespace gilt.dblinq.sensor
{
    /// <summary>
    /// Sensor Repository
    /// </summary>
    public interface ISensorRepository : IGenericRepository<SensorProxy>
    {
    }
}
