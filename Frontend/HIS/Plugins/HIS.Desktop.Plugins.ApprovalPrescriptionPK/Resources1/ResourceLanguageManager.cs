using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ApprovalPrescriptionPK.Resources
{
    public class ResourceLanguageManager
    {
        public static ResourceManager LanguageFormApprovalExportPrescription { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                ResourceLanguageManager.LanguageFormApprovalExportPrescription = new ResourceManager("HIS.Desktop.Plugins.ApprovalPrescriptionPK.Resources1.Lang", typeof(HIS.Desktop.Plugins.ApprovalPrescriptionPK.UCApprovalPrescriptionPK).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
