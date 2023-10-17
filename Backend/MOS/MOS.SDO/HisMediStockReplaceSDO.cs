using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisMediStockReplaceSDO
    {
        public List<L_HIS_EXP_MEST_MEDICINE> MedicineReplaces { get; set; }
        public List<L_HIS_EXP_MEST_MATERIAL> MaterialReplaces { get; set; }
    }
}
