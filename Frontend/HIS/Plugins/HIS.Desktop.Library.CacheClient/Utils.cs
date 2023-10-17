using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Library.CacheClient
{
    public class Utils
    {
        public static long? GetModifyTimeMax<T>(List<T> datas)
        {
            long? modifyTimeNew = 0;
            try
            {
                Type type = typeof(T);
                System.Reflection.PropertyInfo propertyInfoOrderField = type.GetProperty("MODIFY_TIME");
                if (propertyInfoOrderField != null)
                {
                    var tbl = datas.ListToDataTable<T>();
                    var drSort = tbl.Select("1 = 1", "MODIFY_TIME DESC").FirstOrDefault();
                    modifyTimeNew = (drSort != null ? long.Parse((drSort["MODIFY_TIME"] ?? "").ToString()) : 0);
                    modifyTimeNew = ((modifyTimeNew.HasValue && modifyTimeNew > 0) ? modifyTimeNew : Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss")));
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }

            return modifyTimeNew;
        }
    }
}
