using Inventec.Common.Adapter;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Library.CacheClient
{
    public class CacheMonitorSync
    {
        public CacheMonitorSync() { }

        public void Create(string dataKey, bool isReload)
        {
            CommonParam param = new CommonParam();
            MOS.EFMODEL.DataModels.HIS_CACHE_MONITOR rs = null;
            MOS.EFMODEL.DataModels.HIS_CACHE_MONITOR data1 = new MOS.EFMODEL.DataModels.HIS_CACHE_MONITOR();
            try
            {
                var captionData = HIS.Desktop.XmlCacheMonitor.CacheMonitorKeyStore.Get().FirstOrDefault(o => o.CacheMonitorKeyCode == dataKey);
                if (captionData != null)
                {
                    data1 = GetCacheMonitorByKey(dataKey);
                    if (data1 != null)
                    {
                        data1.IS_RELOAD = (short)(isReload ? 1 : 0);
                        rs = new Inventec.Common.Adapter.BackendAdapter(param).Post<MOS.EFMODEL.DataModels.HIS_CACHE_MONITOR>(RequestUriStore.HIS_CACHE_MONITOR_UPDATE, ApiConsumers.MosConsumer, data1, param);
                    }
                    else
                    {
                        data1.IS_RELOAD = (short)(isReload ? 1 : 0);
                        data1.DATA_NAME = dataKey;

                        rs = new Inventec.Common.Adapter.BackendAdapter(param).Post<MOS.EFMODEL.DataModels.HIS_CACHE_MONITOR>(RequestUriStore.HIS_CACHE_MONITOR_CREATE, ApiConsumers.MosConsumer, data1, param);
                    }
                }
                else
                {
                    Inventec.Common.Logging.LogSystem.Info("Key du lieu " + dataKey + " khong duoc khai bao trong file CacheMonitorConfig.xml ");
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        public MOS.EFMODEL.DataModels.HIS_CACHE_MONITOR GetCacheMonitorByKey(string dataKey)
        {
            try
            {
                CommonParam param = new CommonParam();
                MOS.Filter.HisCacheMonitorFilter filter = new MOS.Filter.HisCacheMonitorFilter();
                filter.DATA_NAME = dataKey;
                return new BackendAdapter(param).Get<List<MOS.EFMODEL.DataModels.HIS_CACHE_MONITOR>>(RequestUriStore.HIS_CACHE_MONITOR_GET, ApiConsumers.MosConsumer, filter, param).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
                return null;
            }
        }
    }
}
