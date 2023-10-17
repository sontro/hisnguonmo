using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AppointmentService.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCAppointmentService { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCAppointmentService = new ResourceManager("HIS.Desktop.Plugins.AppointmentService.Resources.Lang", typeof(HIS.Desktop.Plugins.AppointmentService.frmAppointmentService).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
