using Inventec.Common.XmlConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Base
{
    internal class ElementNodeADO : ElementNode
    {
        public bool isConfig { get; set; }


        internal ElementNodeADO()
        {
        }
        internal ElementNodeADO(string key)
        {
            this.Title = key;
            this.isConfig = true;
        }

    }
}
