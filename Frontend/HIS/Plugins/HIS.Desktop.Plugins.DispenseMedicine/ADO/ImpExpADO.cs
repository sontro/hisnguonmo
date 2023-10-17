using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.DispenseMedicine.ADO
{
    public class ImpExpADO
    {
        public bool IsImpMest { get; set; }

        public bool IsExpMest { get; set; }

        public string ImpExpMestCode { get; set; }

        public long? CreateTime { get; set; }
    }
}
