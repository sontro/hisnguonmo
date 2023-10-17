using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.BackendData.ADO
{
    public class AgeADO
    {
        public AgeADO() { }
        public AgeADO(int id, string mota)
        {
            this.Id = id;
            this.MoTa = mota;
        }

        public int Id { get; set; }
        public string MoTa { get; set; }
    }
}
