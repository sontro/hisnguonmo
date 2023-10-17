using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.DrugInterventionInfo.ADO
{
    class WarningADO
    {
        public DrugEnum.WarningCategory Category { get; set; }

        /// <summary>
        /// Nội dung lưu ý
        /// </summary>
        public string Message { get; set; }
    }
}
