using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.EInvoiceCreate.ADO
{
    class TypeADO
    {
        public long ID { get; set; }
        public string NAME { get; set; }

        public TypeADO(int id, string name)
        {
            // TODO: Complete member initialization
            this.ID = id;
            this.NAME = name;
        }
    }
}
