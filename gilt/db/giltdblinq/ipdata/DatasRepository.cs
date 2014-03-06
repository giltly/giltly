using gilt.dblinq.proxy;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace gilt.dblinq.ipdata
{
    /// <summary>
    /// Data Repository
    /// </summary>
    public class DatasRepository : GenericRepository<DataProxy>, IDatasRepository
    {
        /// <summary>
        /// Initialize Data Query
        /// </summary>
        protected override void InitializeQuery()
        {
            _genericQuery = (from s in DataContext.Datas select new DataProxy(s));           
        }

        #region IDatasRepository CRUD
        /// <summary>
        /// Update a Data
        /// </summary>
        /// <param name="DataProxy">Data to Update</param>
        public override void Update(DataProxy DataProxy)
        {
            Data existingData = (from s in DataContext.Datas where s.Id == DataProxy.Id select s).Single();
            existingData.SensorId = DataProxy.SensorId;
            existingData.EventId = DataProxy.EventId;
            existingData.Payload = DataProxy.Payload;
            DataContext.SubmitChanges();
        }
        /// <summary>
        /// Add a Data
        /// </summary>
        /// <param name="DataProxy">Data to Add</param>
        public override void Add(DataProxy DataProxy)
        {
            Data newDatas = new Data();
            newDatas.SensorId = DataProxy.SensorId;
            newDatas.EventId = DataProxy.EventId;
            newDatas.Payload = DataProxy.Payload;
            DataContext.Datas.InsertOnSubmit(newDatas);
            DataContext.SubmitChanges();
        }
        /// <summary>
        /// Delete a Data
        /// </summary>
        /// <param name="DataProxy">Data to Delete</param>
        public override void Delete(DataProxy DataProxy)
        {
            Data datas = (from s in DataContext.Datas where s.Id == DataProxy.Id select s).Single();
            DataContext.Datas.DeleteOnSubmit(datas);
            DataContext.SubmitChanges();
        }
        #endregion

    }
}
