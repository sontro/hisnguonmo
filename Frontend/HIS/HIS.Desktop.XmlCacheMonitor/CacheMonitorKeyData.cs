
using System;
using System.Xml.Serialization;
namespace HIS.Desktop.XmlCacheMonitor
{
    [Serializable]
    public class CacheMonitorKeyData
    {
        [XmlAttribute]
        public string CacheMonitorKeyName { get; set; }
        [XmlAttribute]
        public string CacheMonitorKeyCode { get; set; }
        [XmlAttribute]
        public string IsReload { get; set; }
        [XmlAttribute]
        public string Description { get; set; }

        public CacheMonitorKeyData()
        {
        }

        public CacheMonitorKeyData(string heinMediOrgCode, string heinMediOrgName, string isReload)
        {
            this.CacheMonitorKeyName = heinMediOrgName;
            this.CacheMonitorKeyCode = heinMediOrgCode;
            this.IsReload = isReload;
        }
    }
}
