using gilt.bacon.modules;
using gilt.dblinq.proxy;
using gilt.util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;

namespace gilt.bacon.models
{
    /// <summary>
    /// Model containing Properties that All Pages Get
    /// </summary>
    public class BaseModel
    {        
        /// <summary>
        /// The model of the database user
        /// </summary>
        public UserModel UserModel
        {
            get { return _userModel; }
        }
        private UserModel _userModel = null;

        /// <summary>
        /// A List of the User's Saved Searches
        /// </summary>
        public List<SearchProxy> SavedSearches
        {
            get {return _savedSearches;}
            set { _savedSearches = value; }
        }
        private List<SearchProxy> _savedSearches;

        /// <summary>
        /// The User's currently selected search name
        /// </summary>
        public string ActiveSearchName
        {
            get { return _activeSearchName; }
            set { _activeSearchName = value; }
        }
        private string _activeSearchName;

        /// <summary>
        /// If the User Is Logged into the Website
        /// </summary>
        public List<bool> IsLoggedIn 
        {
            get { return _userModel.IsLoggedIn; }
        }
        /// <summary>
        /// If the User Is a System Administrator
        /// </summary>
        public List<bool> IsAdminUser
        {
            get { return _userModel.IsAdminUser; }
        }

        /// <summary>
        /// Localized Text Strings
        /// </summary>
        public dynamic Text
        {
            get 
            {
                dynamic localizedStringsObject = new ExpandoObject();
                var localizedDictionary =  (IDictionary<string, object>)localizedStringsObject;
                //get the resources from the gilt.bacon dll
                ResourceManager rm = new ResourceManager("gilt.bacon" + ".Resources.Text", Assembly.Load("gilt.bacon"));

                //whatever thus culture is set to is what will be used to display the site
                CultureInfo currentCult = Thread.CurrentThread.CurrentCulture;

                //load up the default values first
                foreach (CultureInfo ci in new List<CultureInfo>() { new CultureInfo("en-US"), currentCult })
                {
                    //get all of the resource strings from the manager
                    ResourceSet rs = rm.GetResourceSet(ci, true, true);
                    foreach (DictionaryEntry entry in rs)
                    {
                        string key = entry.Key.ToString();
                        //add items to the dicitionary which is pinned to the localizedStringsObject and returned
                        //each key is the name column in the resx
                        if (localizedDictionary.ContainsKey (key))
                        {
                            localizedDictionary.Remove(key);
                        }
                        localizedDictionary.Add(key, entry.Value);
                    }
                }
                return localizedStringsObject;
            }
        }

        /// <summary>
        /// Routes for use in the Views
        /// </summary>
        public dynamic Routes
        {
            get
            {
                dynamic routesObject = new ExpandoObject();
                var routesDictionary = (IDictionary<string, object>)routesObject;
                Type routeType = typeof(GiltlyRoutes);
                Dictionary<string,object> routes = routeType.GetFields(BindingFlags.Public | BindingFlags.Static).ToDictionary(x => x.Name, x => x.GetValue(null));
                foreach (string key in routes.Keys)
                {
                    object val = routes[key];
                    routesDictionary.Add(key, routes[key]);
                }
                return routesObject;
            }
        }

        /// <summary>
        /// If the site is in demo mode.  
        /// AKA the demo site on azure
        /// </summary>
        public List<bool> IsDemoSite
        {
            get { return (AppSettings.IsDemoSite ? new List<bool> {true} : null); }
        }
        /// <summary>
        /// Create a base model
        /// </summary>
        public BaseModel()
        {
            _savedSearches = new List<SearchProxy>();
        }
        /// <summary>
        /// Create a base model
        /// </summary>
        /// <param name="User">The user model</param>
        public BaseModel(UserModel User) : this()
        {
            _userModel = User;
        }
        /// <summary>
        /// Create a base model
        /// </summary>
        /// <param name="User">User model</param>
        /// <param name="SavedSearches">Saved Searches</param>
        public BaseModel(UserModel User, List<SearchProxy> SavedSearches)
            : this(User)
        {
            _savedSearches = SavedSearches;
            //_userModel.UserProxy = null when not logged in
            if (null != _userModel.UserProxy)
            {
                if (null != _userModel.UserProxy.ActiveSearch)
                {
                    _activeSearchName = SavedSearches.Where(a => a.Id == _userModel.UserProxy.ActiveSearch).Single().Name;
                }
            }
        }
    }
}