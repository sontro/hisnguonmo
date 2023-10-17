using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisImport.ADO
{
    public class ImportADO : MOS.EFMODEL.DataModels.HIS_EMPLOYEE
    {
        public string IS_DOCTOR_STR { get; set; }
        public string IS_ADMIN_STR { get; set; }
        public string CONG_VIEC { get; set; }
        public string ERROR { get; set; }
    }
}
