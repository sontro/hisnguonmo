using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.PublicMedicineGeneral.ADO
{
    public class ExpMestMediAndMateADO : MOS.EFMODEL.DataModels.V_HIS_EXP_MEST_MEDICINE
    {
        public long Service_Type_Id { get; set; }
        public short? IS_CHEMICAL_SUBSTANCE { set; get; }
        public long INTRUCTION_TIME { get; set; }
        public long INTRUCTION_DATE { get; set; }
        public long TREATMENT_ID { get; set; }
        public int type { get; set; }
    }
}
