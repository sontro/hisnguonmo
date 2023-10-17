using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AssignService.ADO
{
    internal class ServiceGroupADO : HIS_SERVICE_GROUP
    {
        public bool IsChecked { get; set; }
        public string SERVICE_GROUP_NAME__UNSIGN { get; set; }
        public ServiceGroupADO() : base() { }
    }
}
