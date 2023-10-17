using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.PrescriptionAbsentList
{
    public class ConfigADO
    {
        public decimal? rowNumber { get; set; }
        public decimal? CoChuSTT { get; set; }
        public decimal? CoChuTenBN { get; set; }
        public  decimal? CoChuTenQuay { get; set; }
        public bool autoOpenWaitingScreen { get; set; }
        public ConfigADO()
        {

        }
    }
}
