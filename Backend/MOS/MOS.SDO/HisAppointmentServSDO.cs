using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisAppointmentServSDO
    {
        public long TreatmentId { get; set; }
        public List<HIS_APPOINTMENT_SERV> AppointmentServs { get; set; }
    }
}
