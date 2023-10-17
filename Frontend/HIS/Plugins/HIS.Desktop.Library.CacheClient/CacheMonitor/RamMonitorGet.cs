using HIS.Desktop.XmlRamMonitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Library.CacheClient
{
    public class RamMonitorGet
    {
        public RamMonitorGet() { }

        public RamMonitorKeyData GetByCode(string type)
        {
            try
            {
                return HIS.Desktop.XmlRamMonitor.RamMonitorKeyStore.Get().FirstOrDefault(o => o.RamMonitorKeyCode == type);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return null;
        }

        public List<RamMonitorKeyData> Get()
        {
            try
            {
                return HIS.Desktop.XmlRamMonitor.RamMonitorKeyStore.Get();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return null;
        }

        public bool IsExistsCode(string type)
        {
            try
            {
                var xm = HIS.Desktop.XmlRamMonitor.RamMonitorKeyStore.Get().FirstOrDefault(o => o.RamMonitorKeyCode == type);
                return (xm != null && !String.IsNullOrEmpty(xm.RamMonitorKeyCode));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return false;
        }
    }
}
