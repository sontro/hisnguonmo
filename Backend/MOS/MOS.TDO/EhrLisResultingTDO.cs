using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.TDO
{
    public enum EhrGender
    {
        MALE = 0,
        FEMALE = 1,
        OTHER = 2,
        UNKNOWN = 3
    }

    public enum EhrResponseCode
    {
        SUCCESS = 0,
        FAILED = -1,
    }

    public class EhrLisResultTDO
    {
        public string group_name { get; set; }
        public string value { get; set; }
        public string ref_value { get; set; }
    }
    public class EhrPatientTDO
    {
        public string card_code { get; set; }
        public string name { get; set; }
        public string birth_date { get; set; }
        public string address { get; set; }
        public int gender { get; set; }
        public string phone_number { get; set; }

    }
    public class EhrLisResultingTDO
    {
        public EhrPatientTDO subject { get; set; }
        public string identifier { get; set; }
        public string apppointed_doctor { get; set; }
        public string examination_unit { get; set; }
        public string examination_doctor { get; set; }
        public string test_date { get; set; }
        public List<EhrLisResultTDO> result { get; set; }
    }

    public class EhrResponseTDO
    {
        public int code { get; set; }
        public string detail { get; set; }
        public string result { get; set; }
    }
}
