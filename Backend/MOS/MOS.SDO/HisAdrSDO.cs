using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisAdrSDO
    {
        public HIS_ADR Adr { get; set; }
        public List<HIS_ADR_MEDICINE_TYPE> AdrMedicineTypes { get; set; }
    }
}
