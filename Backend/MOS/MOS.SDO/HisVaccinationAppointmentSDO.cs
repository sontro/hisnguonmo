using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class VaccAppointmentDetail
    {
        public long VaccineTypeId { get; set; }
        public long VaccineTurn { get; set; }
    }
    /// <summary>
    /// Ket qua chi dinh vaccin
    /// </summary>
    public class HisVaccinationAppointmentSDO
    {
        public long RequestRoomId { get; set; }
        public long VaccinationExamId { get; set; }
        public string Advise { get; set; }
        public long AppointmentTime { get; set; }
        public List<VaccAppointmentDetail> Details { get; set; }
    }
}
