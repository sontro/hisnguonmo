using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.Dashboard.Filter
{
    public class ServiceReqExamDateFilter
    {
        public long DepartmentId { get; set; }
        public long IntructionDateFrom { get; set; }
        public long IntructionDateTo { get; set; }
    }
}
