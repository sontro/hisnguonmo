using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.EnterKskInfomantionVer2.ADO
{
    class DiseaseTypeADO
    {
        public long ID { get; set; }
        public long PERIOD_DRIVER_DITY_ID { get; set; }
        public string DISEASE_TYPE_NAME { get; set; }
        public bool IS_YES { get; set; }
        public bool IS_NO { get; set; }
    }
}
