using System;

namespace gilt.bacon.modules
{
    /// <summary>
    /// Constant Route Definitions
    /// </summary>
    public static class GiltlyRoutes
    {
        /// <summary>
        /// / 
        /// </summary>
        public const string INDEX = @"/";

        #region Events
        /// <summary>
        /// /Event/
        /// </summary>
        public const string EVENT_ROOT = @"/Event";
        /// <summary>
        /// /Event/List
        /// </summary>
        public const string EVENT_LIST = EVENT_ROOT + @"/List";
        /// <summary>
        /// /Event/ByIpList
        /// </summary>
        public const string EVENT_BY_IP_LIST = EVENT_ROOT + @"/ByIpList";
        /// <summary>
        /// /Event/ByCountryList
        /// </summary>
        public const string EVENT_BY_COUNTRY_LIST = EVENT_ROOT + @"/ByCountryList";
        /// <summary>
        /// /Event/BySourcePortList
        /// </summary>
        public const string EVENT_BY_SOURCEPORT_LIST = EVENT_ROOT + @"/BySourcePortList";
        /// <summary>
        /// /Event/ByDestinationPortList
        /// </summary>
        public const string EVENT_BY_DESTINATIONPORT_LIST = EVENT_ROOT + @"/ByDestinationPortList";        
        /// <summary>
        /// /Event/(?<Id>[0-9]*)
        /// </summary>
        public const string EVENT_BY_ID = EVENT_ROOT + @"/(?<Id>[0-9]*)";
        /// <summary>
        /// /Event/View/(?<Id>[0-9]*)
        /// </summary>
        public const string EVENT_VIEW_BY_ID = EVENT_ROOT + @"/View/(?<Id>[0-9]*)";
        /// <summary>
        /// /Event/PreviousCount
        /// </summary>
        public const string EVENT_PREVIOUS = EVENT_ROOT + @"/PreviousCount/(?<MinutesBackStart>[0-9]*)/(?<MinutesDuration>[0-9]*)";
        /// <summary>
        /// /Event/(?<Page>[0-9]*)/(?<PageSize>[0-9]*)
        /// </summary>
        public static readonly string EVENT_PAGED = String.Format(EVENT_ROOT + @"/{0}", PagingParameters.PAGING_PARAMETERS);
        /// <summary>
        /// /Event/ByIp/
        /// </summary>
        public static readonly string EVENT_BY_IP = String.Format(EVENT_ROOT + @"/ByIp/");
        /// <summary>
        /// /Event/ByIp/(?<Page>[0-9]*)/(?<PageSize>[0-9]*)
        /// </summary>
        public static readonly string EVENT_BY_IP_PAGED = String.Format(@"{0}{1}", EVENT_BY_IP, PagingParameters.PAGING_PARAMETERS);
        /// <summary>
        /// /Event/ByCountry/
        /// </summary>
        public static readonly string EVENT_BY_COUNTRY = String.Format(EVENT_ROOT + @"/ByCountry/");
        /// <summary>
        /// /Event/ByCountry/(?<Page>[0-9]*)/(?<PageSize>[0-9]*)
        /// </summary>
        public static readonly string EVENT_BY_COUNTRY_PAGED = String.Format(@"{0}{1}", EVENT_BY_COUNTRY , PagingParameters.PAGING_PARAMETERS);
        /// <summary>
        /// /Event/ByDestinationPort/
        /// </summary>
        public static readonly string EVENT_BY_DESTINATIONPORT = String.Format(EVENT_ROOT + @"/ByDestinationPort/");
        /// <summary>
        /// /Event/ByDestinationPort/(?<Page>[0-9]*)/(?<PageSize>[0-9]*)
        /// </summary>
        public static readonly string EVENT_BY_DESTINATIONPORT_PAGED = String.Format(@"{0}{1}", EVENT_BY_DESTINATIONPORT, PagingParameters.PAGING_PARAMETERS);
        /// <summary>
        /// /Event/BySourcePort/
        /// </summary>
        public static readonly string EVENT_BY_SOURCEPORT = String.Format(EVENT_ROOT + @"/BySourcePort/");                        
        /// <summary>
        /// /Event/BySourcePort/(?<Page>[0-9]*)/(?<PageSize>[0-9]*)
        /// </summary>
        public static readonly string EVENT_BY_SOURCEPORT_PAGED = String.Format(@"{0}{1}", EVENT_BY_SOURCEPORT, PagingParameters.PAGING_PARAMETERS);                        
        /// <summary>
        /// /Event/GetGeoData
        /// </summary>
        public const string EVENT_GEODATA = EVENT_ROOT + @"/GetGeoData";
        #endregion

        #region EventComment
        /// <summary>
        /// /EventComment
        /// </summary>
        public const string EVENTCOMMENT_ROOT = @"/EventComment";
        /// <summary>
        /// /EventComment/Create
        /// </summary>
        public const string EVENTCOMMENT_CREATE = EVENTCOMMENT_ROOT + @"/Create";
        #endregion

        #region Search
        /// <summary>
        /// /Search
        /// </summary>
        public const string SEARCH_ROOT = @"/Search";
        /// <summary>
        /// /Search/(?<Id>[0-9]*)
        /// </summary>
        public const string SEARCH_BY_ID = SEARCH_ROOT + @"/(?<Id>[0-9]*)";        
        /// <summary>
        /// /Search/Delete/(?<Id>[0-9]*)
        /// </summary>
        public const string SEARCH_DELETE_ID = SEARCH_ROOT + @"/Delete/(?<Id>[0-9]*)";
        /// <summary>
        /// /Search/Update
        /// </summary>
        public const string SEARCH_UPDATE = SEARCH_ROOT + @"/Update";        
        /// <summary>
        /// /Search/Edit/(?<Id>[0-9]*)
        /// </summary>
        public const string SEARCH_EDIT_VIEW = SEARCH_ROOT + @"/Edit/(?<Id>[0-9]*)";
        /// <summary>
        /// /Search/Add
        /// </summary>
        public const string SEARCH_ADD_VIEW = SEARCH_ROOT + @"/Add";        
        #endregion

        #region Auth
        /// <summary>
        /// /login
        /// </summary>
        public const string LOGIN = @"/login";
        /// <summary>
        /// /logout
        /// </summary>
        public const string LOGOUT = @"/logout";
        #endregion

        #region Sensor
        /// <summary>
        /// /Sensor
        /// </summary>
        public const string SENSOR_ROOT = @"/Sensor";
        /// <summary>
        /// /Sensor/List
        /// </summary>
        public const string SENSOR_LIST = SENSOR_ROOT + @"/List";
        /// <summary>
        /// /Sensor/{Page}/{PageSize}
        /// </summary>
        public const string SENSOR_PAGED = SENSOR_ROOT + @"/{Page}/{PageSize}";
        /// <summary>
        /// /Sensort/{Id}
        /// </summary>
        public const string SENSOR_BY_ID = SENSOR_ROOT + @"/{Id}";
        #endregion

        #region Users
        /// <summary>
        /// /Users
        /// </summary>
        public const string USER_ROOT = @"/Users";
        /// <summary>
        /// /Users.Add
        /// </summary>
        public const string USER_ADD = USER_ROOT + @"/Add";
        /// <summary>
        /// /Users/Created
        /// </summary>
        public const string USER_CREATE = USER_ROOT + @"/Create";
        /// <summary>
        /// /Users/List
        /// </summary>
        public const string USER_LIST = USER_ROOT + @"/List";
        /// <summary>
        /// /Users/Profile
        /// </summary>
        public const string USER_PROFILE = USER_ROOT + @"/Profile";
        /// <summary>
        /// /Users/ChangePassword
        /// </summary>
        public const string USER_CHANGEPASSWORD = USER_ROOT + @"/ChangePassword";
        /// <summary>
        /// /Users/Password/Update
        /// </summary>
        public const string USER_PASSWORD_UPDATE = USER_ROOT + @"/Password/Update";
        /// <summary>
        /// /Users/Profile/Update
        /// </summary>
        public const string USER_PROFILE_UPDATE = USER_ROOT + @"/Profile/Update";
        /// <summary>
        /// /Users/Search/SetActive
        /// </summary>
        public const string USER_SEARCH_SET = USER_ROOT + @"/Search/SetActive";
        /// <summary>
        /// /Users/#/(?<Page>[0-9]*)/(?<PageSize>[0-9]*)
        /// </summary>
        public static readonly string USER_PAGED = String.Format(USER_ROOT + @"/{0}", PagingParameters.PAGING_PARAMETERS);
        #endregion

        #region UserGroups
        /// <summary>
        /// /UserGroups
        /// </summary>
        public const string USERGROUP_ROOT = @"/UserGroups";
        /// <summary>
        /// /UserGroups/Assignment/(?<Id>[0-9]*)
        /// </summary>
        public const string USERGROUP_ASSIGNMENT = USERGROUP_ROOT + @"/Assignment/(?<Id>[0-9]*)";
        /// <summary>
        /// /UserGroups/Add
        /// </summary>
        public const string USERGROUP_ADD = USERGROUP_ROOT + @"/Add";
        /// <summary>
        /// /UserGroups/Create
        /// </summary>
        public const string USERGROUP_CREATE = USERGROUP_ROOT + @"/Create";
        /// <summary>
        /// /UserGroups/List
        /// </summary>
        public const string USERGROUP_LIST = USERGROUP_ROOT + @"/List";        
        /// <summary>
        /// /UserGroups/AssignUserGroup
        /// </summary>
        public const string USERGROUP_ASSIGN_USERGROUPS = USERGROUP_ROOT + @"/AssignUserGroups";
        /// <summary>
        /// /UserGroups/AssignData/(?<Id>[0-9]*)
        /// </summary>
        public const string USERGROUP_SIDE_BY_SIDE = USERGROUP_ROOT + @"/AssignData/(?<Id>[0-9]*)";
        /// <summary>
        /// /UserGroups/(?<Page>[0-9]*)/(?<PageSize>[0-9]*)
        /// </summary>
        public static readonly string USERGROUP_PAGED = String.Format(USERGROUP_ROOT + @"/{0}", PagingParameters.PAGING_PARAMETERS);
        #endregion

        #region Roles
        /// <summary>
        /// /Role/
        /// </summary>
        public const string ROLE_ROOT = @"/Role";
        /// <summary>
        /// /Role/Assignment/(?<Id>[0-9]*)
        /// </summary>
        public const string ROLE_ASSIGNMENT = ROLE_ROOT + @"/Assignment/(?<Id>[0-9]*)";
        /// <summary>
        /// /Role/UnAssigned/(?<Id>[0-9]*)
        /// </summary>
        public const string ROLE_UNASSIGNED = ROLE_ROOT + @"/Unassigned/(?<Id>[0-9]*)";
        /// <summary>
        /// /Role/Assigned/(?<Id>[0-9]*)
        /// </summary>
        public const string ROLE_ASSIGNED = ROLE_ROOT + @"/Assigned/(?<Id>[0-9]*)";
        /// <summary>
        /// /Role/AssignData/(?<Id>[0-9]*)
        /// </summary>
        public const string ROLE_SIDE_BY_SIDE = ROLE_ROOT + @"/AssignData/(?<Id>[0-9]*)";
        /// <summary>
        /// /Role/AssignRoles
        /// </summary>
        public const string ROLE_ASSIGN_ROLES = ROLE_ROOT + @"/AssignRoles";

        #endregion

        #region TimeLine
        /// <summary>
        /// /Event/TimelineData
        /// </summary>
        public const string EVENT_TIMELINEDATA = EVENT_ROOT + @"/TimelineData";
        /// <summary>
        /// /Event/Timeline
        /// </summary>
        public const string EVENT_TIMELINE = EVENT_ROOT + @"/Timeline";
        #endregion

        #region Admin
        /// <summary>
        /// /Admin/
        /// </summary>
        public const string ADMIN_ROOT = @"/Admin";
        /// <summary>
        /// /Admin/ResetData
        /// </summary>
        public const string ADMIN_DATA_RESET = ADMIN_ROOT + @"/ResetData";
        /// <summary>
        /// /Admin/DeleteData
        /// </summary>
        public const string ADMIN_DATA_DELETEEVENTS = ADMIN_ROOT + @"/DeleteData";
        /// <summary>
        /// /Admin/EventDelete
        /// </summary>
        public const string ADMIN_EVENT_DELETE = ADMIN_ROOT + @"/EventDelete";
        /// <summary>
        /// /Admin/DeleteEvents
        /// </summary>
        public const string ADMIN_EVENT_DELETEEVENTS = ADMIN_ROOT + @"/DeleteEvents";
        /// <summary>
        /// /Admin/GeoDelete
        /// </summary>
        public const string ADMIN_GEO_DELETE = ADMIN_ROOT + @"/GeoDelete";
        /// <summary>
        /// /Admin/DeleteGeo
        /// </summary>
        public const string ADMIN_GEO_DELETEGEO = ADMIN_ROOT + @"/DeleteGeo";
        /// <summary>
        /// /Admin/SnortDelete
        /// </summary>
        public const string ADMIN_SNORT_DELETE = ADMIN_ROOT + @"/SnortDelete";
        /// <summary>
        /// /Admin/DeleteSnort
        /// </summary>
        public const string ADMIN_SNORT_DELETESNORT = ADMIN_ROOT + @"/DeleteSnort";
        /// <summary>
        /// /Admin/LogDelete
        /// </summary>
        public const string ADMIN_LOG_DELETE = ADMIN_ROOT + @"/LogDelete";
        /// <summary>
        /// /Admin/DeleteLog
        /// </summary>
        public const string ADMIN_LOG_DELETELOG = ADMIN_ROOT + @"/DeleteLog";
        #endregion
    }
}