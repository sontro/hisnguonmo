using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.CareCreate.ADO
{
    public class HisCareDetailADO : MOS.EFMODEL.DataModels.HIS_CARE_DETAIL
    {
        public int Action { get; set; }
        public long CARE_TEMP_ID { get; set; }
    }
}
