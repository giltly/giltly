using gilt.dblinq.proxy;
using gilt.dblinq.user;
using Nancy;
using Nancy.ModelBinding;
using System;

namespace gilt.bacon.modules.users
{
    /// <summary>
    /// User Module
    /// </summary>
    public partial class UsersModule : ModuleBase<IUsersRepository, UsersProxy>
    {
        /// <summary>
        /// User POST Responses
        /// </summary>
        protected override void PostResponses()
        {
            Post[GiltlyRoutes.USER_PROFILE_UPDATE] = parameters =>
            {
                //grab the user guid and set it before update
                this.GetModel();
                UsersProxy requestedUserUpdates = this.Bind<UsersProxy>();
                UsersProxy currentUser = _baseModel.UserModel.UserProxy;

                requestedUserUpdates.Guid = _baseModel.UserModel.UserProxy.Guid;
                requestedUserUpdates.ActiveSearch = currentUser.ActiveSearch;
                requestedUserUpdates.Email = currentUser.Email;
                requestedUserUpdates.Password = currentUser.Password;

                return _crudDelegate(requestedUserUpdates, CrudEnum.Update, GiltlyRoutes.USER_PROFILE);
            };

            Post[GiltlyRoutes.USER_PASSWORD_UPDATE] = parameters =>
            {
                //grab the user guid and set it before update
                this.GetModel();
                UsersProxy requestedUserUpdates = this.Bind<UsersProxy>();
                UsersProxy currentUser = _baseModel.UserModel.UserProxy;
                string password1 = Request.Form["Password1"];
                string password2 = Request.Form["Password2"];

                //existing password is not correct 
                var userGuid = _userMapping.ValidateUser(_baseModel.UserModel.UserProxy.Email, requestedUserUpdates.Password);
                if ((userGuid == null))
                {
                    return Response.AsRedirect(String.Format("{0}?error=true", GiltlyRoutes.INDEX));
                }
                if ((password1 != password2))
                {
                    return Response.AsRedirect(String.Format("{0}?error=true", GiltlyRoutes.INDEX));
                }
                //set the existing GUID so the row can be updated
                requestedUserUpdates.Guid = _baseModel.UserModel.UserProxy.Guid;
                requestedUserUpdates.ActiveSearch = currentUser.ActiveSearch;
                requestedUserUpdates.Email = currentUser.Email;
                requestedUserUpdates.FirstName = currentUser.FirstName;
                requestedUserUpdates.LastName = currentUser.LastName;
                //create the new hash and assign it for save
                requestedUserUpdates.Password = _userMapping.HashUser(_baseModel.UserModel.UserProxy.Email, password1);
                return _crudDelegate(requestedUserUpdates, CrudEnum.Update, GiltlyRoutes.USER_PROFILE);
            };

            Post[GiltlyRoutes.USER_SEARCH_SET] = parameters =>
            {
                //TODO SET SEARCH OFF

                //string searchName = Request.Form["ActiveSearchName"];
                //SearchProxy sp = searchRepository.FindBy(a => a.Name == searchName).FirstOrDefault();
                //if (null != sp)
                //{
                //    //now update the user's active search
                //    UsersProxy requestedUserUpdates = this.GetModel().UserModel.UserProxy;
                //    if (requestedUserUpdates.ActiveSearch == sp.Id)
                //    {
                //        //the user cliced the checkbox that is already checked
                //        requestedUserUpdates.ActiveSearch = null;
                //    }
                //    else
                //    {
                //        requestedUserUpdates.ActiveSearch = sp.Id;
                //    }
                //    return _crudDelegate(requestedUserUpdates, CrudEnum.Update, GiltlyRoutes.INDEX);
                //}
                return Response.AsJson(new { });
            };

            Post[GiltlyRoutes.USER_CREATE] = parameters =>
            {
                //grab the user guid and set it before update
                this.GetModel();
                UsersProxy requestedUserUpdates = this.Bind<UsersProxy>();
                string password1 = Request.Form["Password1"];
                string password2 = Request.Form["Password2"];

                //existing password is not correct 
                var userGuid = _userMapping.ValidateUser(_baseModel.UserModel.UserProxy.Email, requestedUserUpdates.Password);
                if ((userGuid != null))
                {
                    return Response.AsRedirect(String.Format("{0}?error=true", GiltlyRoutes.USER_CREATE));
                }
                if ((password1 != password2))
                {
                    return Response.AsRedirect(String.Format("{0}?error=true", GiltlyRoutes.USER_CREATE));
                }
                //set the existing GUID so the row can be updated
                requestedUserUpdates.Guid = Guid.NewGuid();
                requestedUserUpdates.ActiveSearch = null;
                requestedUserUpdates.CreatedOn = DateTime.Now;
                //create the new hash and assign it for save
                requestedUserUpdates.Password = _userMapping.HashUser(requestedUserUpdates.Email, password1);
                return _crudDelegate(requestedUserUpdates, CrudEnum.Add, GiltlyRoutes.USER_ADD);
            };
        }
    }
}
