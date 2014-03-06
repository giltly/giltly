using System.Collections.Generic;

namespace gilt.bacon.modules.usergroups
{
    public class UserGroupSideBySideItemList
    {
        public int UserId { get; set; }
        public List<UserGroupSideBySideItem> AssignedItems { get; set; }
        public List<UserGroupSideBySideItem> UnAssignedItems { get; set; }
    }
}