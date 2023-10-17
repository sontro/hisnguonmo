using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AssignPrescriptionCLS.ADO
{
    public class HisExpMestMedicineADO : MOS.EFMODEL.DataModels.V_HIS_EXP_MEST_MEDICINE
    {
        public long INTRUCTION_TIME { get; set; }
        public long? REMEDY_COUNT { get; set; }
        public long SERVICE_REQ_ID { get; set; }
    }
}
