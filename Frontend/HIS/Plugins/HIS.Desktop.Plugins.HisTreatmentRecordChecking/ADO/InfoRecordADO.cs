using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisTreatmentRecordChecking.ADO
{
    class InfoRecordADO
    {
        public long DOCUMENT_TYPE_ID { get; set; }
        public string CODE { get; set; }
        public string TYPE { get; set; }
        public string CREATE_TIME_STR { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public string SEARCH_CODE { get; set; }
        public long REQ_TYPE_STT_ID { get; set; }
        public string CREATOR { get; set; }
    }
}
