using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisCheckBeforeTransfusionBlood
{
    public class ADO
    {
        public decimal ID { get; set; }
        public string VALUE { get; set; }

        public ADO(decimal ID, string Val)
        {
            this.ID = ID;
            this.VALUE = Val;
        }
    }
}
