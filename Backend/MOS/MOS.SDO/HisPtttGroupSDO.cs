using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisPtttGroupSDO
    {
        public HIS_PTTT_GROUP HisPtttGroup { get; set; }
        public List<HIS_PTTT_GROUP_BEST> HisPtttGroupBests { get; set; }
    }
}
