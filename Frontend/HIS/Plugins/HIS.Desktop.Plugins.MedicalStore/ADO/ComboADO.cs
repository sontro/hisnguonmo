using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MedicalStore.ADO
{
    public class ComboADO
    {
        public long ID { get; set; }
        public string NAME { get; set; }

        public ComboADO()
        { }

        public ComboADO(long id, string name)
        {
            this.ID = id;
            this.NAME = name;
        }
    }
}
