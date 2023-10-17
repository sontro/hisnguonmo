using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HIS.Desktop.Plugins.TreatmentFinish.CloseTreatment
{
    class IcdUtil
    {
        internal const string seperator = ";";
        internal static string AddSeperateToKey(string key)
        {
            try
            {
                return String.Format("{0}{1}{2}", seperator, key, seperator);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return "";
        }
    }
}
