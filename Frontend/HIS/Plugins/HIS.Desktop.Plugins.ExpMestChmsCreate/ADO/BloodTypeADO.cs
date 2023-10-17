using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestChmsCreate.ADO
{
    public class BloodTypeADO
    {
        public string BLOOD_TYPE_CODE { get; set; }
        public string BLOOD_TYPE_NAME { get; set; }
        public decimal VOLUME { get; set; }
        public decimal AMOUNT { get; set; }
        public string SERVICE_UNIT_NAME { get; set; }
        public string BLOOD_ABO_CODE { get; set; }
        public string BLOOD_RH_CODE { get; set; }
    }
}
