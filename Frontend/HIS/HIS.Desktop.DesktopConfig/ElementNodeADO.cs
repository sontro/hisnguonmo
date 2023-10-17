using Inventec.Common.XmlConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.DesktopConfig
{
    internal class ElementNodeADO : ElementNode
    {
        public bool isConfig { get; set; }
        public ConfigType ConfigType { get; set; }
        public long ID { get; set; }
        public string IP { get; set; }
        public string ConfigFileName { get; set; }
        public ElementNodeADO()
        {
        }
        public ElementNodeADO(string key)
        {
            this.Title = key;
            this.isConfig = true;
        }

    }
}
