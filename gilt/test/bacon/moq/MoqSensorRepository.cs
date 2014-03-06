using gilt.dblinq;
using gilt.dblinq.proxy;
using gilt.dblinq.sensor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace gilt.test.moq
{
    public class MoqSensorRepository : GenericRepository<SensorProxy>, ISensorRepository
    {
        public MoqSensorRepository()
        {
        }
        protected override void InitializeQuery()
        {
            _genericQuery = (IQueryable<SensorProxy>)new List<SensorProxy>
            {
                new SensorProxy( new Sensor())
            }.AsEnumerable<SensorProxy>().AsQueryable<SensorProxy>();
        }

        #region IGenericRepository virtuals
        public override IEnumerable<SensorProxy> FindBy(Func<SensorProxy, bool> predicate)
        {
            return _genericQuery.Where(predicate);
        }
        public override IQueryable<SensorProxy> GetAll()
        {
            return _genericQuery.Where(a => true);
        }
        public override int GetCount()
        {
            return this.GetAll().Count();
        }
        #endregion

        #region CRUD virtuals
        public override void Update(SensorProxy proxy)
        {
        }
        public override void Add(SensorProxy proxy)
        {
        }
        public override void Delete(SensorProxy proxy)
        {
        }
        #endregion
    }
}
