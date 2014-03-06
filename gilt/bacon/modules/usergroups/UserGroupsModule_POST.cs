using gilt.dblinq.proxy;
using gilt.dblinq.user;
using Nancy.Cookies;
using Nancy.ModelBinding;
using Nancy.Responses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace gilt.bacon.modules.usergroups
{
    /// <summary>
    /// UsersGroup Module
    /// </summary>
    public partial class UsersGroupsModule : ModuleBase<IUserGroupsRepository, UserGroupsProxy>
    {
        /// <summary>
        /// UserGroups POST Responses
        /// </summary>
        protected override void PostResponses()
        {
            Post[GiltlyRoutes.USERGROUP_CREATE] = parameters =>
            {
                UserGroupsProxy requestedUserGroupUpdates = this.Bind<UserGroupsProxy>();
                //no updates are requrired to the model before adding -- form is complete
                return _crudDelegate(requestedUserGroupUpdates, CrudEnum.Add, GiltlyRoutes.USER_ADD);
            };

            Post[GiltlyRoutes.USERGROUP_ASSIGN_USERGROUPS] = parameters =>
            {
                //bind the form post
                UserGroupSideBySideItemList createdUserGroupAssignment = this.Bind<UserGroupSideBySideItemList>();

                var response = new RedirectResponse(GiltlyRoutes.USERGROUP_ASSIGNMENT);
                string toastName = "Message";
                string toastValue = "Successfully Updated";
                try
                {
                    //1. get existing useruser groups
                    List<UserUserGroupProxy> currentUserGroups = _temp.FindBy(a => a.UserId == createdUserGroupAssignment.UserId).ToList();

                    //2. remove all assigned userggroups                
                    foreach (UserUserGroupProxy urp in currentUserGroups)
                    {
                        _temp.Delete(urp);
                    }
                    //3. add any new roles                
                    foreach (UserGroupSideBySideItem assignedUserUserGroup in createdUserGroupAssignment.AssignedItems)
                    {
                        _temp.Add(new UserUserGroupProxy(new gilt.dblinq.UserUserGroup { UserId = createdUserGroupAssignment.UserId, UserGroupId = assignedUserUserGroup.Id }));
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
