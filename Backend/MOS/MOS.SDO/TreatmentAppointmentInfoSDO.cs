using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class TreatmentAppointmentInfoSDO
    {
        public long TreatmentId { get; set; }
        //Thong tin hen kham
        public long? AppointmentTime { get; set; }
        public List<long> AppointmentExamRoomIds { get; set; }
        public long? AppointmentPeriodId { get; set; }
        public string Advise { get; set; }
    }
}
