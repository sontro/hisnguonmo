using Inventec.Common.Xml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace HIS.Desktop.XmlRamMonitor
{
    [XmlRootAttribute("RamMonitorKeyLoadConfig", Namespace = "", IsNullable = false)]
    public class RamMonitorKeyLoadConfig
    {
        [XmlAttributeAttribute(DataType = "date")]
        public DateTime DateTimeValue;

        // Serializes an ArrayList as a "RamMonitorKeyList" array of XML elements of custom type RamMonitorKeyData named "RamMonitorKeyData".
        [XmlArray("RamMonitorKeyList"), XmlArrayItem("RamMonitorKeyData", typeof(RamMonitorKeyData))]
        public ArrayList CacheMonitorKeyList = new ArrayList();
    }
}
