using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisImportTestIndexRange.ADO
{
    public class TestIndexRangeADO : MOS.EFMODEL.DataModels.HIS_TEST_INDEX_RANGE
    {
        public string TEST_INDEX_CODE { get; set; }
        public string TEST_INDEX_NAME { get; set; }
        public string AGE_FROM_STR { get; set; }
        public string AGE_TO_STR { get; set; }
        public string IS_FEMALE_STR { get; set; }
        public bool IS_FEMALE_B { get; set; }
        public string IS_MALE_STR { get; set; }
        public bool IS_MALE_B { get; set; }
        public string IS_ACCEPT_EQUAL_MAX_STR { get; set; }
        public bool IS_ACCEPT_EQUAL_MAX_B { get; set; }
        public string IS_ACCEPT_EQUAL_MIN_STR { get; set; }
        public bool IS_ACCEPT_EQUAL_MIN_B { get; set; }
        public string VALUE_RANGE_CHECK { get; set; }

        public string ERROR { get; set; }
    }
}
