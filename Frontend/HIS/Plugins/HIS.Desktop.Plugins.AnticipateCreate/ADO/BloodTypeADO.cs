using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AnticipateCreate.ADO
{
    public class BloodTypeADO: MOS.EFMODEL.DataModels.V_HIS_BLOOD_TYPE
    {
        public long IdRow { get; set; }
        public decimal? AMOUNT { get; set; }
        public long Type { get; set; }
    }
}
