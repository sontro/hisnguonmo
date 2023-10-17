using Inventec.Common.XmlConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.DesktopConfig.ADO
{
    public class ElementNodeADO : ElementNode
    {
        public bool isConfig { get; set; }


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
