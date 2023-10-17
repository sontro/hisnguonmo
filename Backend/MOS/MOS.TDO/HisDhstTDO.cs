using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.TDO
{
    public class HisDhstTDO
    {
        public double? BMI { get; set; }
        public double? CanNang { get; set; }
        public double? ChieuCao { get; set; }
        public int DhstId { get; set; }
        public DateTime? ExecuteTime { get; set; }
        public string HuyetAp { get; set; }
        public bool IsCare { get; set; }
        public bool IsTracking { get; set; }
        public int? Mach { get; set; }
        public double? NhietDo { get; set; }
        public int? NhipTho { get; set; }

        public string TreatmentCode { get; set; }
    }
}
