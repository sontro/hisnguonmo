using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TreatmentHistory.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCBedRoomPartial { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCBedRoomPartial = new ResourceManager("HIS.Desktop.Plugins.TreatmentHistory.Resources.Lang", typeof(HIS.Desktop.Plugins.TreatmentHistory.frmTreatmentHistory).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
