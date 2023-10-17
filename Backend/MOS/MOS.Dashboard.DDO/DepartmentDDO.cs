using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.Dashboard.DDO
{
    public class DepartmentDDO
    {
        public long ID { get; set; }
        public Nullable<short> IS_ACTIVE { get; set; }
        public string DEPARTMENT_CODE { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public long BRANCH_ID { get; set; }
        public string BHYT_CODE { get; set; }
        public string PHONE { get; set; }
    }
}
