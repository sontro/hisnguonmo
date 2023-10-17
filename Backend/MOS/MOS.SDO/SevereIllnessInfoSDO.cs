using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class SevereIllnessInfoSDO
    {
        public HIS_SEVERE_ILLNESS_INFO SevereIllnessInfo { get; set; }
        public List<HIS_EVENTS_CAUSES_DEATH> EventsCausesDeaths { get; set; }
    }
}
