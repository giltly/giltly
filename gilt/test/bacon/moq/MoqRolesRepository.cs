using gilt.dblinq;
using gilt.dblinq.roles;
using gilt.dblinq.proxy;
using System;
using System.Collections.Generic;
using System.Linq;

namespace gilt.test.moq
{
    public class MoqRolesRepository : GenericRepository<RolesProxy>, IRolesRepository
    {
        public MoqRolesRepository()
        {
        }

        protected override void InitializeQuery()
        {
            _genericQuery = (IQueryable<RolesProxy>)new List<RolesProxy>
            {
                new RolesProxy( new Role())
            }.AsEnumerable<RolesProxy>().AsQueryable<RolesProxy>();
        }

        #region IRolesRepository
        public IEnumerable<RolesProxy> GetUnassignedRoles(int UserId)
        {
            return this.GetAll();
        }
        public IEnumerable<RolesProxy> GetAssignedRoles(int UserId)
        {
            return this.GetAll();
        }
        #endregion


        #region IGenericRepository virtuals
        public override IEnumerable<RolesProxy> FindBy(Func<RolesProxy, bool> predicate)
        {
            return _genericQuery.Where(predicate);
        }
        public override IQueryable<RolesProxy> GetAll()
        {
            return _genericQuery.Where(a => true);
        }
        public override int GetCount()
        {
            return this.GetAll().Count();
        }
        #endregion

        #region CRUD virtuals
        public override void Update(RolesProxy proxy)
        {
        }
        public override void Add(RolesProxy proxy)
        {
        }
        public override void Delete(RolesProxy proxy)
        {
        }
        #endregion
    }
}
