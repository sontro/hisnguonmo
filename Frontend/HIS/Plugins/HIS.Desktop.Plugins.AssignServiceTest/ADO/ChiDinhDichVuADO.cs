using MPS.ADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AssignServiceTest.ADO
{
    public class ChiDinhDichVuADO
    {
        public string bedRoomName { get; set; }
        public string firstExamRoomName { get; set; }
        public MOS.EFMODEL.DataModels.HIS_TREATMENT treament { get; set; }
        public MOS.EFMODEL.DataModels.V_HIS_SERVICE_REQ ServiceReqPrint { get; set; }
        public MOS.EFMODEL.DataModels.V_HIS_PATIENT_TYPE_ALTER patientTypeAlter { get; set; }
    }
}
