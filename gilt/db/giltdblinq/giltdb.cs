using System.Configuration;
using System.IO;
using gilt.util;

namespace gilt.dblinq
{
    partial class giltdbDataContext
    {
        public giltdbDataContext()
            : base("")
        {
            OnCreated();
        }
    }
}
