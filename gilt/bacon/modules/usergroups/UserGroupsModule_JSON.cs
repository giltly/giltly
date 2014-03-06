using gilt.dblinq.proxy;
using gilt.dblinq.user;
using Nancy;
using System;
using System.Linq;
using System.Linq.Dynamic;

namespace gilt.bacon.modules.usergroups
{
    public partial class UsersGroupsModule : ModuleBase<IUserGroupsRepository, UserGroupsProxy>
    {
        protected override void JsonResponses()
        {
            Get[GiltlyRoutes.USERGROUP_PAGED] = parameters =>
            {
                PagingParameters pp = new PagingParameters(parameters);
                int pageSize = pp.PageSize;
                int page = pp.PageNumber;

                SortParameters sp = new SortParameters(this.Request.Query);

                IQueryable<UserGroupsProxy> filteredEvents = _repository.FindBy(a => 1 == 1).AsQueryable();
                int eventCount = filteredEvents.Count();
                var rows = filteredEvents.OrderBy(String.Format("{0} {1}", sp.SortName, sp.SortDirection)).Skip(page * pageSize).Take(pageSize)
                    .Select(x =>
                        new
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Description = x.Description
                        });
                int totalPages = (int)Math.Ceiling((float)eventCount / (float)pageSize);

                return Response.AsJson(
                    new
                    {
                        Rows = rows,
                        NumberPages = totalPages
                    });
            };

            Get[GiltlyRoutes.USERGROUP_SIDE_BY_SIDE] = parameters =>
            {
                int id = parameters.Id;
                var unassignedRows = _repository.GetUnassignedUserGroups(id);
                var assignedRows = _repository.GetAssignedUserGroups(id);
                return Response.AsJson(
                    new
                    {
                        UnAssigned = unassignedRows,
                        Assigned = assignedRows
                    });
            };  
        } 
    }
}
