using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Text;
using System.Linq;

namespace MOS.UTILITY
{
    public class CommonUtil
    {
        public static decimal NVL(decimal? value)
        {
            return NVL(value, 0);
        }

        public static decimal NVL(decimal? value, decimal val)
        {
            return value.HasValue ? value.Value : val;
        }

        public static string NVL(string input, string separator)
        {
            return !string.IsNullOrWhiteSpace(input) ? string.Format("{0}{1}", input, separator) : "";
        }

        public static string NVL(string input)
        {
            return !string.IsNullOrWhiteSpace(input) ? input : "";
        }

        /// <summary>
        /// Kiem tra xem 2 danh sach co khac nhau khong
        /// </summary>
        /// <param name="lst1"></param>
        /// <param name="lst2"></param>
        /// <returns></returns>
        public static bool IsDiff<T>(List<T> lst1, List<T> lst2)
        {
            if (lst1 != null && lst1.Count > 0 && (lst2 == null || lst2.Count == 0))
            {
                return true;
            }
            if (lst2 != null && lst2.Count > 0 && (lst1 == null || lst1.Count == 0))
            {
                return true;
            }
            return lst1.Exists(t => !lst2.Contains(t)) || lst2.Exists(t => !lst1.Contains(t));
        }

        /// <summary>
        /// Kiem tra xem d/s sources co chua d/s destinations hay ko
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sources"></param>
        /// <param name="destinations"></param>
        /// <returns></returns>
        public static bool Contains<T>(List<T> sources, List<T> destinations)
        {
            if (sources == null || destinations == null)
            {
                return false;
            }
            if (sources.Count < destinations.Count)
            {
                return false;
            }
            foreach(T t in destinations)
            {
                if (!sources.Contains(t))
                {
                    return false;
                }
            }
            return true;
        }

        public static string ToString(decimal number)
        {
            return number.ToString(CultureInfo.CreateSpecificCulture("en-US"));
        }

        public static string ToUpper(string input)
        {
            return input != null ? input.ToUpper() : null;
        }

        public static void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
        {
            var expandoDict = expando as IDictionary<string, object>;
            if (expandoDict.ContainsKey(propertyName))
                expandoDict[propertyName] = propertyValue;
            else
                expandoDict.Add(propertyName, propertyValue);
        }

        public static string SubString(string input, int maxLength)
        {
            string rs = input;
            if (!String.IsNullOrWhiteSpace(input) && Encoding.UTF8.GetByteCount(input) > maxLength)
            {
                for (int i = input.Length - 1; i >= 0; i--)
                {
                    if (Encoding.UTF8.GetByteCount(input.Substring(0, i + 1)) <= maxLength)
                    {
                        rs = String.Format("{0}", input.Substring(0, i + 1));
                        break;
                    }
                }
            }
            return rs;
        }

        public static void AddParamInfo(CommonParam source, CommonParam dest)
        {
            if (source != null && dest != null)
            {
                if (source.BugCodes != null && source.BugCodes.Count > 0)
                {
                    dest.BugCodes.AddRange(source.BugCodes);
                }
                if (source.Messages != null && source.Messages.Count > 0)
                {
                    dest.Messages.AddRange(source.Messages);
                }
            }
        }

        public static bool HasChar(string p)
        {
            if (!String.IsNullOrWhiteSpace(p))
            {
                p = p.Trim();
                foreach (char c in p)
                {
                    if (!char.IsNumber(c))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool IsListIdsContainsId(string ids, long id)
        {
            bool valid = false;
            try
            {
                if (String.IsNullOrWhiteSpace(ids))
                {
                    valid = false;
                }
                else
                {
                    var listIds = ids.Split(',');
                    if (listIds != null && listIds.Contains(id.ToString()))
                        valid = true;
                }
            }
            catch (Exception ex)
            {
                valid = false;
            }
            return valid;
        }
    }
}
