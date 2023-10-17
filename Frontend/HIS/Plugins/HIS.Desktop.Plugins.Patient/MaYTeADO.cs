using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Patient
{
    public class MaYTeADO
    {
        public long ID { get; set; }
        public string Name { get; set; }

        public MaYTeADO(long id, string name)
        {
            this.ID = id;
            this.Name = name;
        }
    }
}
