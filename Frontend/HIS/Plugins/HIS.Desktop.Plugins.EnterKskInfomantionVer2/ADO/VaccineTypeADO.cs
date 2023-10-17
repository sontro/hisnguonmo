using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.EnterKskInfomantionVer2.ADO
{
    class VaccineTypeADO
    {
        public long ID { get; set; }
        public long UNEI_VATY_ID { get; set; }
        public string VACCINE_TYPE_NAME { get; set; }
        public long? CONDITION_TYPE { get; set; }
        public bool IS_YES { get; set; }
        public bool IS_NO { get; set; }
        public bool IS_FORGOT { get; set; }
    }
}
