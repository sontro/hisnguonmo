using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RegisterExamKiosk.ADO
{
    public class RightRouteTypeADO
    {
        public string RightRouteTypeCode { get; set; }
        public string RightRouteTypeName { get; set; }
        public long ID { get; set; }

        public RightRouteTypeADO()
        {
        }

        public RightRouteTypeADO(long ID, string rightRouteTypeCode, string rightRouteTypeName)
        {
            this.RightRouteTypeCode = rightRouteTypeCode;
            this.RightRouteTypeName = rightRouteTypeName;
            this.ID = ID;
        }
    }
}
