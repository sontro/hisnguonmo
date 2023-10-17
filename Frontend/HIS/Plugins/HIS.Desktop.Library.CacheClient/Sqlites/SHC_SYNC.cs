using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Library.CacheClient.Sqlites
{
    public class SHC_SYNC
    {
        public SHC_SYNC() { }

        public string KEY { get; set; }
        public long LAST_DB_MODIFY_TIME { get; set; }
        public long LAST_SYNC_MODIFY_TIME { get; set; }
    }
}
