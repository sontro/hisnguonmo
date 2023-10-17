
using System;
using System.Xml.Serialization;
namespace HIS.Desktop.XmlRamMonitor
{
    [Serializable]
    public class RamMonitorKeyData
    {
        [XmlAttribute]
        public string RamMonitorKeyName { get; set; }
        [XmlAttribute]
        public string RamMonitorKeyCode { get; set; }

        public RamMonitorKeyData()
        {
        }

        public RamMonitorKeyData(string code, string name)
        {
            this.RamMonitorKeyName = name;
            this.RamMonitorKeyCode = code;
        }
    }
}
