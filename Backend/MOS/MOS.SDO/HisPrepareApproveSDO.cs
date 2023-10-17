using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisPrepareApproveSDO
    {
        public long Id { get; set; }
        public long ReqRoomId { get; set; }
        public List<HisPrepareMatySDO> PrepareMatys { get; set; }
        public List<HisPrepareMetySDO> PrepareMetys { get; set; }
    }
}
