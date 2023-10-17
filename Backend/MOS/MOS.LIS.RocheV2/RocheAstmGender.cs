using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LIS.RocheV2
{
    /// <summary>
    /// F: female, M: male, U: unknown
    /// </summary>
    public enum RocheAstmGender
    {
        FEMALE = 'F',
        MALE = 'M',
        UNKNOWN = 'U'
    }

    public class GenderUtil
    {
        public static RocheAstmGender ToGender(string gender)
        {
            if (gender != null)
            {
                if (gender.Equals("F"))
                {
                    return RocheAstmGender.FEMALE;
                }
                else if (gender.Equals("M"))
                {
                    return RocheAstmGender.MALE;
                }
            }
            return RocheAstmGender.UNKNOWN;
        }

        public static string ToString(RocheAstmGender gender)
        {
            return ((char)gender).ToString();
        }
    }
}
