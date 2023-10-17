using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class MrCheckSummarySDO
    {
        public HIS_MR_CHECK_SUMMARY HisMrCheckSummary { get; set; }
        public List<HIS_MR_CHECKLIST> HisMrChecklists { get; set; }
    }
}
