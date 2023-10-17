using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionList.ADO
{
    class LoaiHoaDon
    {
         public long ID { get; set; }
        public string Name { get; set; }
         public LoaiHoaDon(){}
         public bool Check { get; set; }
         public LoaiHoaDon(long id, string filterTypeName, bool CHECK)
        {
            this.ID = id;
            this.Name = filterTypeName;
            this.Check = CHECK;
        }
    }
}
