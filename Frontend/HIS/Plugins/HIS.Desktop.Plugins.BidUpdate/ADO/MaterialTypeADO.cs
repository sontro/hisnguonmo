using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BidUpdate.ADO
{
    public class MaterialTypeADO: MOS.EFMODEL.DataModels.V_HIS_MATERIAL_TYPE
    {
        public long IdRow { get; set; }
        public decimal? AMOUNT { get; set; }
        public decimal? ADJUST_AMOUNT { get; set; }
        public long Type { get; set; }
        public string BID_NUM_ORDER { get; set; }
        public string BID_GROUP_CODE { get; set; }
        public string BID_PACKAGE_CODE { get; set; }
        public bool IsMaterialTypeMap { get; set; }


        public long? MONTH_LIFESPAN { get; set; }
        public long? DAY_LIFESPAN { get; set; }
        public long? HOUR_LIFESPAN { get; set; }

        public string NOTE { get; set; }


    }
}
