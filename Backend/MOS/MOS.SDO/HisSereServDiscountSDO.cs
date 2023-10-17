using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisSereServDiscountSDO
    {
        public List<HIS_SERE_SERV> HisSereServs { get; set; }
        public long TreatmentId { get; set; }
        public bool? IsAutoDiscount { get; set; }
        public decimal? AutoDiscountRatio { get; set; }
    }
}
