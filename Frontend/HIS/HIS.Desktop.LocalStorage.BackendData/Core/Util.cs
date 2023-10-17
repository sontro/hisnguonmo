using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.LocalStorage.BackendData
{
    class Util
    {       
        public static string ToCamelCase(string input)
        {
            if (input != null)
            {
                return input.ToLower().Split(new[] { "_" }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => char.ToUpperInvariant(s[0]) + s.Substring(1, s.Length - 1))
                    .Aggregate(string.Empty, (s1, s2) => s1 + s2);
            }
            return null;
        }

        public static string FirstCharUpper(string input)
        {
            string result = null;
            if (input != null)
            {
                result = input.Substring(0, 1).ToUpper();
                if (input.Length > 1)
                {
                    result += input.Substring(1, input.Length - 1);
                }
            }
            return result;
        }

        public static string FirstCharLower(string input)
        {
            string result = null;
            if (input != null)
            {
                result = input.Substring(0, 1).ToLower();
                if (input.Length > 1)
                {
                    result += input.Substring(1, input.Length - 1);
                }
            }
            return result;
        }
    }
}
