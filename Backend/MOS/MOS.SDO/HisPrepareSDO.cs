using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisPrepareSDO
    {
        public long? Id { get; set; }
        public long TreatmentId { get; set; }
        public long? FromTime { get; set; }
        public long? ToTime { get; set; }
        public string ReqLoginname { get; set; }
        public string ReqUsername { get; set; }
        public string Description { get; set; }
        public long? ReqRoomId { get; set; }

        public List<HIS_PREPARE_MATY> MaterialTypes { get; set; }
        public List<HIS_PREPARE_METY> MedicineTypes { get; set; }
    }
}
