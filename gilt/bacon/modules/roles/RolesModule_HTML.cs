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
        protected override void HtmlResponses()
        {
            Get[GiltlyRoutes.ROLE_ASSIGNMENT] = parameters =>
            {
                return this.RenderView("RoleAssignment");
            };            
        }
    }
}
