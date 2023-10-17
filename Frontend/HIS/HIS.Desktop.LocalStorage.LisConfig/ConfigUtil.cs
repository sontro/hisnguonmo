using Inventec.Common.Logging;
using LIS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.LisConfig
{
    public class ConfigUtil
    {
        public static long GetLongConfig(string code)
        {
            long result = 0;
            try
            {
                result = long.Parse(GetStrConfig(code));
            }
            catch (Exception ex)
            {
                LogSystem.Error("Loi khi lay Config: " + code, ex);
                result = 0;
            }
            return result;
        }

        public static decimal GetDecimalConfig(string code)
        {
            decimal result = 0;
            try
            {
                result = decimal.Parse(GetStrConfig(code));
            }
            catch (Exception ex)
            {
                LogSystem.Error("Loi khi lay Config: " + code, ex);
                result = 0;
            }
            return result;
        }

        public static int GetIntConfig(string code)
        {
            int result = 0;
            try
            {
                result = int.Parse(GetStrConfig(code));
            }
            catch (Exception ex)
            {
                LogSystem.Error("Loi khi lay Config: " + code, ex);
                result = 0;
            }
            return result;
        }

        public static string GetStrConfig(string code)
        {
            string result = null;
            try
            {
                LIS_CONFIG config = null;
                object data = null;
                if (!LisConfigs.dic.TryGetValue(code, out data))
                {
                    LogSystem.Info("ConfigUtil => Khong Get duoc cau hinh theo Key: " + code);
                }
                else
                {
                    config = data as LIS_CONFIG;
                }
                if (config == null) throw new ArgumentNullException(code);
                result = !String.IsNullOrEmpty(config.VALUE) ? config.VALUE : (!String.IsNullOrEmpty(config.DEFAULT_VALUE) ? config.DEFAULT_VALUE : "");
            }
            catch (Exception ex)
            {
                LogSystem.Warn("Loi khi lay Config: " + code, ex);
            }
            return result;
        }

        public static List<string> GetStrConfigs(string code)
        {
            List<string> result = new List<string>();
            try
            {
                string str = GetStrConfig(code);
                string[] arr = str.Split(',');
                if (arr != null)
                {
                    foreach (string s in arr)
                    {
                        result.Add(s);
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return result;
        }
    }
}
