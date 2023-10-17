using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.BackendData.ADO
{
    public class ServiceReq1ADO : MOS.EFMODEL.DataModels.HIS_SERVICE_REQ
    {
        public bool CallPatientSTT { get; set; }

        public bool IsCalling { get; set; }

        public long? VaccinationId { get; set; }
        public string TDL_PATIENT_NAME_OLD { get; set; }

        public ServiceReq1ADO()
        {

        }
    }
}
