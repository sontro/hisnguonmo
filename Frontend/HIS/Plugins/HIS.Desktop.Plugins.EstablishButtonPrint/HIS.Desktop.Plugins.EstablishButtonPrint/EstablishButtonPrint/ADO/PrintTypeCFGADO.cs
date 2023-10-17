using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.EstablishButtonPrint.EstablishButtonPrint.ADO
{
    public class PrintTypeCFGADO
    {
        public short? IS_ACTIVE { get; set; }
        public string APP_CODE { get; set; }
        public string MODULE_LINK { get; set; }
        public string CONTROL_PATH { get; set; }
        public string CONTROL_CODE { get; set; }
        public string BRANCH_CODE { get; set; }
        public PrintTypeIDCaptionADO PRINT_TYPE_CAPTION { get; set; }
        public List<PrintTypeIDCaptionADO> PRINT_TYPE_CAPTIONs { get; set; }
        public string CREATOR { get; set; }
        public long? CREATE_TIME { get; set; }
        public string MODIFIER { get; set; }
        public long? MODIFY_TIME { get; set; }

    }
}
