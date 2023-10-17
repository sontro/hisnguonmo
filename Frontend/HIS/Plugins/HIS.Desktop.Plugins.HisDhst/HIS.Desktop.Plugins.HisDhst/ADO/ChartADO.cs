using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisDhst.ADO
{
    public class ChartADO
    {
        public string Date { get; set; }
        public string DateTime { get; set; }
        public long? PULSE { get; set; }
        public decimal? TEMPERATURE { get; set; }
        public decimal? BLOOD_PRESSURE_MAX { get; set; }
    }
}
