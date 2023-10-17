using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TYT.Desktop.Plugins.TytDeathCreate
{
    class DeathLocationTypeADO
    {
        public string NAME { get; set; }
        public long ID { get; set; }

        public DeathLocationTypeADO(long ID, string NAME)
        {
            this.ID = ID;
            this.NAME = NAME;
        }
    }
}
