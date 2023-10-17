using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.TreatmentEndTypeExt.Data
{
    public class BabyADO
    {
        public long? id { get; set; }
        public string FatherName { get; set; }
        public long? GenderId { get; set; }
        public decimal? Weight { get; set; }
        public long? BornTime { get; set; }
        public DateTime? BornTimeDt { get; set; }
    }
}
