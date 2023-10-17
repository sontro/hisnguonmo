using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MedicineTypeCreate.ADO
{
    class VHisServicePatyADO : MOS.EFMODEL.DataModels.V_HIS_SERVICE_PATY
    {
        public int STT { get; set; }
        public int Action { get; set; }
        public DateTime DT_FROM_TIME { get; set; }
        public DateTime DT_TO_TIME { get; set; }
    }
}
