using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisImpMestDonationSDO
    {
        public HIS_IMP_MEST ImpMest { get; set; }
        public List<DonationDetailSDO> DonationDetail { get; set; }
    }

    public class DonationDetailSDO
    {
        public HIS_BLOOD_GIVER BloodGiver { get; set; }
        public List<HIS_BLOOD> Bloods { get; set; }
    }
}
