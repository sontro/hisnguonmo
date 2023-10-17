using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class HisRehaSumSDO : HIS_REHA_SUM
    {
        public List<long> ServiceReqIds { get; set; }
    }
}
