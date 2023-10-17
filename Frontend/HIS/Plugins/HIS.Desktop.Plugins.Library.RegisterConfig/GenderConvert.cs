using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.RegisterConfig
{
    public class GenderConvert
    {
        public static string TextToNumber(string ge)
        {
            return (ge == "Nữ") ? "2" : "1";
        }

        public static string HisToHein(string ge)
        {
            return (ge == "1") ? "2" : "1";
        }

        public static long HeinToHisNumber(string ge)
        {
            return (ge == "1" ? IMSys.DbConfig.HIS_RS.HIS_GENDER.ID__MALE : IMSys.DbConfig.HIS_RS.HIS_GENDER.ID__FEMALE);
        }
    }
}
