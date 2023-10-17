using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LIS.RocheV3
{
    /// <summary>
    /// F: female, M: male, U: unknown
    /// </summary>
    public enum RocheHl7Gender
    {
        FEMALE = 'F',
        MALE = 'M',
        UNKNOWN = 'U'
    }

    public class GenderUtil
    {
        public static RocheHl7Gender ToGender(string gender)
        {
            if (gender != null)
            {
                if (gender.Equals("F"))
                {
                    return RocheHl7Gender.FEMALE;
                }
                else if (gender.Equals("M"))
                {
                    return RocheHl7Gender.MALE;
                }
            }
            return RocheHl7Gender.UNKNOWN;
        }

        public static string ToString(RocheHl7Gender gender)
        {
            return ((char)gender).ToString();
        }
    }
}
