using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisNumOrderBlockChooser.Base
{
    class GlobalVariablesProcess
    {
        internal static string GenerateHour(string hour)
        {
            return new StringBuilder().Append(hour.Substring(0, 2)).Append(":").Append(hour.Substring(2, 2)).ToString();
        }
    }
}
