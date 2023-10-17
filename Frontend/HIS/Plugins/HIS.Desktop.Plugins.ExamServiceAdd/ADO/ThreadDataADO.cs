using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExamServiceAdd.ADO
{
    class ThreadDataADO
    {
        public ThreadDataADO(V_HIS_SERVICE_REQ ServiceReqPrint)
        {
            this.VHisServiceReq_print = ServiceReqPrint;
        }

        public V_HIS_SERVICE_REQ VHisServiceReq_print { get; set; }
        public V_HIS_PATIENT VHisPatient { get; set; }
        public V_HIS_PATIENT_TYPE_ALTER VHisPatientTypeAlter { get; set; }
        public V_HIS_SERE_SERV VHisSereServ { get; set; }
        public decimal Ratio { get; set; }
        public string FirstExamRoom { get; set; }
    }
}
