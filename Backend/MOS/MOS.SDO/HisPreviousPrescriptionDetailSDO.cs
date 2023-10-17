using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisPreviousPrescriptionDetailSDO
    {
        public long REQUEST_ROOM_ID { get; set; }
        public string REQUEST_ROOM_NAME { get; set; }
        public string TREATMENT_CODE { get; set; }
        public string SERVICE_REQ_CODE { get; set; }
        public long INTRUCTION_TIME { get; set; }
        public List<PreviousPrescriptionMedicineSDO> ExpMedicines { get; set; }
    }

    public class PreviousPrescriptionMedicineSDO
    {
        public string MEDICINE_TYPE_CODE { get; set; }
        public string MEDICINE_TYPE_NAME { get; set; }
        public long? USE_TIME_TO { get; set; }
    }

    public class PreviousPrescriptionDetailResultSDO : HisPreviousPrescriptionSDO
    {
        public string MEDICINE_TYPE_CODE { get; set; }
        public string MEDICINE_TYPE_NAME { get; set; }
    }
}
