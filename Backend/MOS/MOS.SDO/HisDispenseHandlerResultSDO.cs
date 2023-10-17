using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisDispenseHandlerResultSDO : HisDispenseResultSDO
    {
        public List<HIS_EXP_MEST_MEDICINE> ExpMestMedicines { get; set; }
        public List<HIS_EXP_MEST_MATERIAL> ExpMestMaterials { get; set; }
        public HIS_IMP_MEST_MEDICINE ImpMestMedicine { get; set; }
        public List<HIS_MEDICINE_PATY> MedicinePaties { get; set; }
    }
}
