using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AssignPrescriptionCLS.ADO
{
    public class StringUtil
    {
        public static string convertToUnSign3(string s)
        {
            string result = null;
            try
            {
                Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
                if (!string.IsNullOrEmpty(s))
                {
                    string temp = s.Normalize(NormalizationForm.FormD);
                    result = regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
                }
            }
            catch (Exception ex)
            {
                result = null;
                //Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return result;
        }
    }
}
