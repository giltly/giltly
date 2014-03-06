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
        protected override void PostResponses()
        {
            Post[GiltlyRoutes.ROLE_ASSIGN_ROLES] = parameters =>
            {
                //bind the form post
                RoleSideBySideItemList createdRoleAssignment = this.Bind<RoleSideBySideItemList>();

                var response = new RedirectResponse(GiltlyRoutes.ROLE_ASSIGNMENT);
                string toastName = "Message";
                string toastValue = "Successfully Updated";
                try
                {
                    //1. get existing user roles
                    List<UserRolesProxy> currentUserRoles = _userRolesRepository.FindBy(a => a.UserId == createdRoleAssignment.UserId).ToList();

                    //2. remove all assigned roles                
                    foreach (UserRolesProxy urp in currentUserRoles)
                    {
                        _userRolesRepository.Delete(urp);
                    }
                    //3. add any new roles                
                    foreach (RoleSideBySideItem assignedRole in createdRoleAssignment.AssignedItems)
                    {
                        _userRolesRepository.Add(new UserRolesProxy(new gilt.dblinq.UserRole { UserId = createdRoleAssignment.UserId, RoleId = assignedRole.Id }));
                    }
                }
                catch (Exception exc)
                {
                    _giltLogger.Error(exc.ToString());
                    toastName = "Error";
                    toastValue = "Error Performing Action";
                }
                finally
                {
                    var cookie = new NancyCookie(toastName, toastValue, false, false);
                    response.AddCookie(cookie);
                }
                return response;
            };
        }
    }
}
