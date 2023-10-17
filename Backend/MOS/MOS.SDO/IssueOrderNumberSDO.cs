using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class IssueOrderNumberSDO
    {
        public long OrderNumber { get; set; }
        public string DepartmentName { get; set; }
        public string RegisterGateName { get; set; }
        public string RegisterGateAddress { get; set; }
    }
}
