using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ModuleButtonCustomize
{
    class SdaRequestUriStore
    {
        internal const string SDA_CUSTOMIZE_BUTTON_CREATE = "/api/SdaCustomizeButton/Create";
        internal const string SDA_CUSTOMIZE_BUTTON_DELETE = "/api/SdaCustomizeButton/Delete";
        internal const string SDA_CUSTOMIZE_BUTTON_UPDATE = "/api/SdaCustomizeButton/Update";
        internal const string SDA_CUSTOMIZE_BUTTON_GET = "/api/SdaCustomizeButton/Get";
        internal const string SDA_CUSTOMIZE_BUTTON_CHANGE_LOCK = "/api/SdaCustomizeButton/ChangeLock";
        internal const string ACS_CUSTOMIZE_MODULE = "/api/AcsModule/Get";
        internal const string SDA_HIDE_CONTROL_GET = "/api/SdaHideControl/Get";
    }
}
