using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.EFMODEL.DataModels;
using ACS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.HisImportEmpUser.ADO
{
    class EmpUserADO : ACS_USER
    {
        public string ERROR { get; set; }
        public string DIPLOMA { get; set; }
        public string IS_ADMIN_STR { get; set; }
        public string IS_DOCTOR_STR { get; set; }
        public string CONG_VIEC { get; set; }
        public string IS_NURSE_STR { get; set; }
        public string MEDICINE_TYPE_RANK { get; set; }
        public string ACCOUNT_NUMBER { get; set; }
        public string BANK { get; set; }
        public string DEPARTMENT_CODE { get; set; }
        public string MAX_BHYT { get; set; }
       
    }
}
