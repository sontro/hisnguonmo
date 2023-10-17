using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.PrintBordereau.Base
{
    public class GlobalDataStore
    {
        public static PrintOption.Value CURRENT_PRINT_OPTION { get; set; }

        internal static string PrinterName { get; set; }
    }
}
