using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.PrintPrescription
{
    class Config
    {
        internal const string mps = "HIS.Desktop.Plugins.Library.PrintPrescription.Mps";

        internal const string CONFIG_KEY__PRINT_BARCODE_NO_ZERO = "HIS.Desktop.Library.Print.BacodeNoZero";

        internal const string TAKE_INFO_TT_CLS = "HIS.DESKTOP.PRINT_PRESCRIPTION.TAKE_INFO_TT_CLS";

        internal const string KEY_IsPrintPrescriptionNoThread = "HIS.Desktop.IsPrintPrescriptionNoThread";

        internal const string ASSIGN_PRESCRIPTION_ODER_OPTION = "HIS.Desktop.Plugins.AssignPrescription.OderOption";
   
        internal static bool IsmergePrint
        {
            get
            {
                return HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>("HIS.Desktop.Plugins.OptionMergePrint.Ismerge") == "1";
            }
        }
    }
}
