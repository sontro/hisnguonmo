using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.DrugInterventionInfo.ADO
{
    class IssueCategoryADO
    {
        public DrugEnum.ValidationSeverityLevel Severity { get; set; }
        public string CategoryName { get; set; }
        public List<IssueTextADO> Issues { get; set; }
    }
}
