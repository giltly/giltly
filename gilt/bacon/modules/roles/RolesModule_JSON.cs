using gilt.dblinq.proxy;
using gilt.dblinq.roles;
using gilt.dblinq.search;
using Nancy;
using Nancy.Cookies;
using Nancy.ModelBinding;
using Nancy.Responses;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;

namespace gilt.bacon.modules.roles
{
    public partial class RolesModule : ModuleBase<IRolesRepository, RolesProxy>
    {
        protected override void JsonResponses()
        {
            Get[GiltlyRoutes.ROLE_ASSIGNED] = parameters =>
            {
                int id = parameters.Id;
                var rows = _repository.GetAssignedRoles(id);
                return Response.AsJson(
                    new
                    {
                        Rows = rows
                    });
            };

            Get[GiltlyRoutes.ROLE_UNASSIGNED] = parameters =>
            {
                int id = parameters.Id;
                var rows = _repository.GetUnassignedRoles(id);
                return Response.AsJson(
                    new
                    {
                        Rows = rows
                    });
            };

            Get[GiltlyRoutes.ROLE_SIDE_BY_SIDE] = parameters =>
            {
                int id = parameters.Id;
                var unassignedRows = _repository.GetUnassignedRoles(id);
                var assignedRows = _repository.GetAssignedRoles(id);
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
