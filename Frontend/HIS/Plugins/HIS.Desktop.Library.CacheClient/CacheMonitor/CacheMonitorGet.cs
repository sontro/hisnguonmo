using HIS.Desktop.XmlCacheMonitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Library.CacheClient
{
    public class CacheMonitorGet
    {
        public CacheMonitorGet() { }

        public CacheMonitorKeyData GetByCode(string type)
        {
            try
            {
                return HIS.Desktop.XmlCacheMonitor.CacheMonitorKeyStore.Get().FirstOrDefault(o => o.CacheMonitorKeyCode == type);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return null;
        }

        public List<CacheMonitorKeyData> Get()
        {
            try
            {
                return HIS.Desktop.XmlCacheMonitor.CacheMonitorKeyStore.Get();
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
                var xm = HIS.Desktop.XmlCacheMonitor.CacheMonitorKeyStore.Get().FirstOrDefault(o => o.CacheMonitorKeyCode == type);
                return (xm != null && !String.IsNullOrEmpty(xm.CacheMonitorKeyCode));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return false;
        }
    }
}
