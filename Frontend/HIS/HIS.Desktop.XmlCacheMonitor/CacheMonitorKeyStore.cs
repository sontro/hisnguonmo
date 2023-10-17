using Inventec.Common.Logging;
using Inventec.Common.Xml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace HIS.Desktop.XmlCacheMonitor
{
    public class CacheMonitorKeyStore
    {
        private static readonly List<CacheMonitorKeyData> CACHE_MONITOR_KEY_STORE = InitDataFromXml(XmlCacheMonitorConfig.DATA_CONFIG_FILE_PATH);
        static CacheMonitorKeyLoadConfig CacheMonitorKeyLoadConfig;
        static string XmlFilePath;
        public static List<CacheMonitorKeyData> Get()
        {
            return CACHE_MONITOR_KEY_STORE;
        }

        public static CacheMonitorKeyData GetByCode(string code)
        {
            try
            {
                return code != null && CACHE_MONITOR_KEY_STORE != null ?
                    CACHE_MONITOR_KEY_STORE.Where(o => o.CacheMonitorKeyCode != null && o.CacheMonitorKeyCode.Equals(code)).FirstOrDefault() : null;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        public static bool UpdateByCode(string code, string newValue)
        {
            try
            {
                var data = (code != null && CACHE_MONITOR_KEY_STORE != null) ?
                    CACHE_MONITOR_KEY_STORE.Where(o => o.CacheMonitorKeyCode != null && o.CacheMonitorKeyCode.Equals(code)).FirstOrDefault() : null;
                data.IsReload = newValue;
                ObjectXMLSerializer<CacheMonitorKeyLoadConfig>.Save(CacheMonitorKeyLoadConfig, XmlFilePath);
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        public static bool IsValidCode(string code)
        {
            if (GetByCode(code) == null)
            {
                LogSystem.Error("Ma 'Du lieu cache' khong hop le");
                return false;
            }
            return true;
        }

        private static List<CacheMonitorKeyData> InitDataFromXml(string xmlFilePath)
        {
            List<CacheMonitorKeyData> result = null;
            try
            {
                XmlFilePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(ApplicationStoreLocation.ApplicationStartupPath, xmlFilePath));
                CacheMonitorKeyLoadConfig = ObjectXMLSerializer<CacheMonitorKeyLoadConfig>.Load(XmlFilePath);
                result = CacheMonitorKeyLoadConfig != null ? CacheMonitorKeyLoadConfig.CacheMonitorKeyList.Cast<CacheMonitorKeyData>().ToList() : null;
            }
            catch (Exception ex)
            {
                LogSystem.Error("Loi khi load du lieu cache local bao gom cac bang du lieu lon, du lieu dung chung, cac du lieu danh muc it bien dong, cac (phuc vu viec lay du lieu tu local ma khong can goi len server MOS) tu file cau hinh XML____xmlFilePath:" + xmlFilePath, ex);
            }
            return result;
        }
    }
}
