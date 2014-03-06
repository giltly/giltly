using gilt.dblinq;
using gilt.dblinq.search;
using gilt.bacon.auth;
using gilt.bacon.models;
using NLog;
using Nancy;
using Nancy.Cookies;
using Nancy.Responses;
using Nancy.Responses.Negotiation;
using System;
using System.Linq;

namespace gilt.bacon.modules
{
    public abstract class ModuleBase<T,U> : NancyModule where T : IGenericRepository<U> where U : class
    {
        protected T _repository;        
        protected ISearchRepository _searchRepository;
        protected BaseModel _baseModel = null;
        protected CrudDelegate _crudDelegate = null;
        protected delegate Response CrudDelegate(U Proxy, CrudEnum CrudMethod, string ReDirectURI = GiltlyRoutes.INDEX);
        protected Logger _giltLogger = LogManager.GetLogger("database");
        private UserModel _loggedInUser = null;

        public ModuleBase(T repository, ISearchRepository searchRepository)
        {
            _repository = repository;            
            _searchRepository = searchRepository;
            //create the instance of the crud method
            _crudDelegate = CrudMethod;

            //create all of the NancyModule.RouteBuilder handlers
            this.HtmlResponses();
            this.JsonResponses();
            this.PostResponses();
        }

        /// <summary>
        /// Create and return the model that all pages use
        /// </summary>
        /// <returns></returns>
        protected BaseModel GetModel()
        {
            if (null == _loggedInUser)
            {
                _loggedInUser = new UserModel((GiltyUserIdentity)this.Context.CurrentUser);                
                _baseModel = new BaseModel(_loggedInUser, _searchRepository.GetAll().ToList());
            }
            return _baseModel;
        }

        /// <summary>
        /// The Id of the current logged in user
        /// </summary>
        public int CurrentLoggedInUserId
        {
            get
            {
                return this.GetModel().UserModel.UserProxy.Id;
            }            
        }

        /// <summary>
        /// Return a web page view with model information
        /// </summary>
        /// <param name="ViewName"></param>
        /// <returns></returns>
        protected Negotiator RenderView(string ViewName)
        {
            return View[ViewName, this.GetModel()];
        }        

        /// <summary>
        /// Transparently, Handles, Add, Delete, Update from A Proxy Object
        /// Repositories are responsible for Implementing the Actual Logic
        /// </summary>
        /// <param name="Proxy"></param>
        /// <param name="CrudMethod"></param>
        /// <returns></returns>
        protected virtual Response CrudMethod(U Proxy, CrudEnum CrudMethod, string ReDirectURI = GiltlyRoutes.INDEX)
        {
            var response = new RedirectResponse(ReDirectURI);
            string toastName = "Message";
            string toastValue = "";
            try
            {
                switch (CrudMethod)
                {
                    case CrudEnum.Add:
                        _repository.Add(Proxy);
                        toastValue = "Successfully Added";
                        break;
                    case CrudEnum.Delete:
                        _repository.Delete(Proxy);
                        toastValue = "Successfully Deleted";
                        break;
                    case CrudEnum.Update:
                        _repository.Update(Proxy);
                        toastValue = "Successfully Updated";
                        break;
                    default:
                        throw new NotImplementedException();                        
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
        }

        #region Virtual  NancyModule.RouteBuilder Methods that define the HTML, JSON, and POST
        protected virtual void HtmlResponses() { }
        protected virtual void JsonResponses() { }
        protected virtual void PostResponses() { }
        #endregion
    }
}