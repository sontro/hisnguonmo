using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisCacheMonitor
{
    public class CacheMonitorADO : MOS.EFMODEL.DataModels.HIS_CACHE_MONITOR
    {
        public CacheMonitorADO() { }
        public string DATA_DISPLAY { get; set; }
    }
}
