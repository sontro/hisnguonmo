using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.EstablishButtonPrint
{
    class HisRequestUriStore
    {
        internal const string MOSSAR_PRINT_TYPE_CFG_CREATE = "api/SarPrintTypeCfg/CreateList";
        internal const string MOSSAR_PRINT_TYPE_CFG_DELETE = "api/SarPrintTypeCfg/DeleteList";
        internal const string MOSSAR_PRINT_TYPE_CFG_UPDATE = "api/SarPrintTypeCfg/UpdateList";
        internal const string MOSSAR_PRINT_TYPE_CFG_GET = "api/SarPrintTypeCfg/Get";
        internal const string MOSSAR_PRINT_TYPE_CFG_CHANGE_LOCK = "api/SarPrintTypeCfg/ChangeLockByControl";
    }
}
