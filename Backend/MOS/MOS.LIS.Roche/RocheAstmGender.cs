using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LIS.Roche
{
    //F: female, M: male, U: unknown
    public enum RocheAstmGender
    {
        FEMALE,
        MALE,
        UNKNOWN
    }

    public class GenderUtil
    {
        public static string ToString(RocheAstmGender gender)
        {
            switch (gender)
            {
                case RocheAstmGender.FEMALE:
                    return "F";
                case RocheAstmGender.MALE:
                    return "M";
                default:
                    return "U";
            }
        }

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
    }
}
