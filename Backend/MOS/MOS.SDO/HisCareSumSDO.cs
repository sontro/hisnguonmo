using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class HisCareSumSDO : HIS_CARE_SUM
    {
        public List<long> CareIds { get; set; }
    }
}
