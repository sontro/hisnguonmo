using His.Bhyt.InsuranceExpertise.LDO;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RegisterExamKiosk.ADO
{
    public class InformationObjectADO
    {
        //lấy thông tin BHYT (trái tuyến, giới thiệu)
        public HisExamRegisterKioskSDO ExamRegisterKiosk { get; set; }

        public ResultHistoryLDO HeinInfo { get; set; }
        
        //có dữ liệu khi đã từng đăng ký trên HIS
        public HisPatientForKioskSDO PatientForKiosk { get; set; }

        //có khi quẹt thẻ. kể cả chưa từng đăng ký trên his.
        public HisCardSDO CardInfo { get; set; }
        public string ServiceCode { get; set; }

        public InformationObjectADO(string serviceCode = null)
        {
            this.ServiceCode = serviceCode;
        }
    }
}
