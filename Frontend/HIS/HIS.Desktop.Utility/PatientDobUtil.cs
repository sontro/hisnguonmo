using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Utility
{
    public class PatientDobUtil
    {
        public static string PatientDobRawToDob(string patientDobRaw)
        {
            string result = "";
            try
            {
                patientDobRaw = patientDobRaw.Trim();
                if (patientDobRaw.Length == 8)
                {
                    string ngay = patientDobRaw.Substring(0, 2);
                    string thang = patientDobRaw.Substring(2, 2);
                    string nam = patientDobRaw.Substring(4, 4);
                    result = ngay + "/" + thang + "/" + nam;
                }
            }
            catch (Exception ex)
            {
                result = "";
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return result;
        }

        public static string PatientDobToDobRaw(string patientDob)
        {
            string result = "";
            try
            {
                if (!String.IsNullOrWhiteSpace(patientDob))
                {
                    Char[] patientDobChar = patientDob.ToArray();
                    foreach (var item in patientDobChar)
                    {
                        if (item != '/')
                        {
                            result += item;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                result = "";
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return result;
        }
    }
}
