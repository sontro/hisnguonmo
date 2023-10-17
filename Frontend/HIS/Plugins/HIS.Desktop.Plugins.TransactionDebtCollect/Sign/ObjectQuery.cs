using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionDebtCollect.Sign
{
    class ObjectQuery
    {
        internal static void AddObjectKeyIntoListkey<T>(T data, Dictionary<string, object> keyValues)
        {
            try
            {
                AddObjectKeyIntoListkey(data, keyValues, true);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        internal static void AddObjectKeyIntoListkey<T>(T data, Dictionary<string, object> keyValues, bool isOveride)
        {
            try
            {
                PropertyInfo[] pis = typeof(T).GetProperties();
                if (pis != null && pis.Length > 0)
                {
                    foreach (var pi in pis)
                    {
                        var searchKey = keyValues.SingleOrDefault(o => o.Key == pi.Name);
                        if (searchKey.Key == null)
                        {
                            keyValues.Add(pi.Name, (data != null ? pi.GetValue(data) : null));
                        }
                        else
                        {
                            if (isOveride)
                                keyValues[searchKey.Key] = (data != null ? pi.GetValue(data) : null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
