using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class RationScheduleSDO
    {
        public long ReqRoomId { get; set; }//phong yeu cau
        public long TreatmentId { get; set; }
        public long RationTimeId { get; set; }//bua an
        public long PatientTypeId { get; set; }
        public long ServiceId { get; set; }//suat an
        public long Amount { get; set; }
        public long RefectoryRoomId { get; set; }//nha an
        public long FromTime { get; set; }
        public long? ToTime { get; set; }
        public bool IsForHomie { get; set; }//nguoi nha
        public bool HalfInFirstDay { get; set; }//an tu chieu
        public string Note { get; set; }
        public long? RationScheduleId { get; set; }
    }
}
