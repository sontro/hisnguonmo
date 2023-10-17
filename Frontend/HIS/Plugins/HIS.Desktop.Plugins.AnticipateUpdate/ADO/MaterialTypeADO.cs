using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AnticipateUpdate.ADO
{
    public class MaterialTypeADO: MOS.EFMODEL.DataModels.V_HIS_MATERIAL_TYPE
    {
        public long IdRow { get; set; }
        public decimal? AMOUNT { get; set; }
        public long Type { get; set; }
        public string BID_NUM_ORDER { get; set; }
    }
}
