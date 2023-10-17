using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MaterialTypeCreate
{
   internal static class CustomParse
    {
       internal static decimal? ConvertCustom(string incomingValue)
        {
            decimal val = 0;
            try
            {
                if (!decimal.TryParse(incomingValue, NumberStyles.Number, CultureInfo.InvariantCulture, out val))
                    return null;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return val;
        }
    }
}
