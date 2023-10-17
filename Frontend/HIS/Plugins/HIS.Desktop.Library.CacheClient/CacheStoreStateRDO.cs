using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Library.CacheClient.Redis
{
    public class CacheStoreStateRDO
    {
        public CacheStoreStateRDO() { }
        public string Key { get; set; }
        public long LastDBModifyTime { get; set; }
        public long LastSyncRamModifyTime { get; set; }
        public bool IsWaitingSync { get; set; }
    }
}
