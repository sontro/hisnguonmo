using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
    public class AppointmentServiceADO
    {      
        public long TreatmentId { get; set; }
        public Action<List<MOS.EFMODEL.DataModels.HIS_APPOINTMENT_SERV>> ActSelect { get; set; }

        public AppointmentServiceADO()
        {
          
        }
    }
}
