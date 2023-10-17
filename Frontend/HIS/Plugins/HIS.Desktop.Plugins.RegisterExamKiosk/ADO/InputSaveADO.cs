using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RegisterExamKiosk.ADO
{
    public class InputSaveADO
    {
        public string IcdCode { get; set; }
        public string IcdName { get; set; }
        public string MediOrgCode { get; set; }
        public string MediOrgName { get; set; }
        public long? ReasonId { get; set; }
        public long? FormId { get; set; }
        public string InCode { get; set; }
        public string RightRouteTypeCode { get; set; }
    }
}
