using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisAllergyCardResultSDO
    {
        public V_HIS_ALLERGY_CARD HisAllergyCard { get; set; }
        public List<HIS_ALLERGENIC> HisAllergenics { get; set; }
    }
}
