using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisAllergyCardSDO
    {
        public HIS_ALLERGY_CARD AllergyCard { get; set; }
        public List<HIS_ALLERGENIC> Allergenics { get; set; }
    }
}
