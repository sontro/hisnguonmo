using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisAdrResultSDO
    {
        public V_HIS_ADR HisAdr { get; set; }
        public List<V_HIS_ADR_MEDICINE_TYPE> HisAdrMedicineTypes { get; set; }
    }
}
