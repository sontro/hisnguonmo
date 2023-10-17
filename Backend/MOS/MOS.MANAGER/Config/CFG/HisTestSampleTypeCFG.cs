using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisTestSampleType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.Config.CFG
{
    class HisTestSampleTypeCFG
    {
        private static List<HIS_TEST_SAMPLE_TYPE> data;
        public static List<HIS_TEST_SAMPLE_TYPE> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisTestSampleTypeGet().Get(new HisTestSampleTypeFilterQuery());
                }
                return data;
            }
        }
        public static void Reload()
        {
            var tmp = new HisTestSampleTypeGet().Get(new HisTestSampleTypeFilterQuery());
            data = tmp;
        }
    }
}
