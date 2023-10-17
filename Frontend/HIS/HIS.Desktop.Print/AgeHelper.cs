using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Print
{
    public class AgeHelper
    {
        public static string CalculateAgeFromYear(long ageYearNumber)
        {
            string tuoi = "";
            try
            {
                DateTime? dtAge = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(ageYearNumber);
                long age = DateTime.UtcNow.Year - dtAge.Value.Year;
                if (age <= 0)
                {
                    age = 1;
                }
                tuoi = string.Format("{0:00}", age);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
                tuoi = "";
            }
            return tuoi;
        }
    }
}
