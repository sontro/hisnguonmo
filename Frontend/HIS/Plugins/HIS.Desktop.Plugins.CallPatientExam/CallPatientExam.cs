using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.CallPatientDepartment.ADO
{
    public class CallPatientExamm
    {
     
        public long ID { get; set; }
        public long TYPE_ID { get; set; }
        public long SUM_WAIT_PATIENT { get; set; }
        public long SUM_DOINHG_PATIENT { get; set; }
        public long SUM_DONE_PATIENT { get; set; }
        public string STT_PATIENT { get; set; }
        public long SUM_PATIENT { get; set; }
        public string NameRoom { get; set; }
        public CallPatientExamm() { }

        public CallPatientExamm(HIS_SERVICE_REQ hisserrq)
        {
            try
            {
                if (hisserrq != null)
                {
                    this.TYPE_ID = 1;
                    this.ID = hisserrq.ID;   
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        public CallPatientExamm(HIS_VACCINATION_EXAM hisvacation)
        {
            try
            {
                if (hisvacation != null)
                {
                    this.ID = hisvacation.ID;
                    this.TYPE_ID = 2;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

    }
}
