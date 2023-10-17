using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.LocalCacheManager
{
    public class CacheData
    {
        public CacheData()
        {
        }

        public string LastModifyTimeInRam { get; set; }
        public string LastModifyTimeSync { get; set; }
        public string LastModifyTimeInDB { get; set; }
        public string ObjectName { get; set; }
        public Type ObjectType { get; set; }
        public string Description { get; set; }
        public string ISTL { get; set; }
    }
}
