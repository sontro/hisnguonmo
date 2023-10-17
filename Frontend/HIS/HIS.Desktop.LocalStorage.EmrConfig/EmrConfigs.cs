using Inventec.Common.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.EmrConfig
{
    public class EmrConfigs
    {
        internal static ConcurrentDictionary<string, object> dic = new ConcurrentDictionary<string, object>();
        //private static Object thisLock = new Object();

        /// <summary>
        /// Lay du lieu cua cau hinh tren hisconfig theo chuoi key
        /// </summary>
        /// <typeparam name="T">(Gia tri cua mot trong các kieu du lieu: string, int, long, decimal, Emrt of string)</typeparam>
        /// <param name="key">Mot chuoi key trong ConfigKeys tuong ung voi key cau hinh tren hisconfig</param>
        /// <returns>value</returns>
        public static T Get<T>(string key)
        {
            T result = default(T);
            try
            {
                Type type = typeof(T);
                object data = null;
                if (type == typeof(List<string>))
                {
                    data = ConfigUtil.GetStrConfigs(key);
                }
                else
                {
                    data = ConfigUtil.GetStrConfig(key);
                }
                result = (T)System.Convert.ChangeType(data, typeof(T));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Debug("Get cau hinh EmrConfig theo key that bai, key = " + key, ex);
            }

            return result;
        }
    }
}
