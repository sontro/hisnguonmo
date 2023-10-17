using Inventec.Common.Xml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace HIS.Desktop.XmlCacheMonitor
{
    [XmlRootAttribute("CacheMonitorKeyLoadConfig", Namespace = "", IsNullable = false)]
    public class CacheMonitorKeyLoadConfig
    {
        [XmlAttributeAttribute(DataType = "date")]
        public DateTime DateTimeValue;

        // Serializes an ArrayList as a "CacheMonitorKeyList" array of XML elements of custom type CacheMonitorKeyData named "CacheMonitorKeyData".
        [XmlArray("CacheMonitorKeyList"), XmlArrayItem("CacheMonitorKeyData", typeof(CacheMonitorKeyData))]
        public ArrayList CacheMonitorKeyList = new ArrayList();
    }
}
