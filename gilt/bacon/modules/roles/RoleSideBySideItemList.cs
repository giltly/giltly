using System.Collections.Generic;

namespace gilt.bacon.modules.roles
{
    public class RoleSideBySideItemList
    {
        public int UserId { get; set; }
        public List<RoleSideBySideItem> AssignedItems { get; set; }
        public List<RoleSideBySideItem> UnAssignedItems { get; set; }
    }
}