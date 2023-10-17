using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.DrugInterventionInfo.ADO
{
    class IssueTextADO
    {
        /// <summary>
        /// Có cần hiện cảnh báo hay không
        /// </summary>
        public bool TriggerAlert { get; set; }

        /// <summary>
        /// Nội dung cảnh báo
        /// </summary>
        public string Issue { get; set; }
    }
}
