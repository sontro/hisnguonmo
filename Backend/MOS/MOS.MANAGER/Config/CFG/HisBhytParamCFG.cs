using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisBhytParam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.Config
{
    class HisBhytParamCFG
    {
        private static List<HIS_BHYT_PARAM> data;
        public static List<HIS_BHYT_PARAM> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisBhytParamGet().Get(new HisBhytParamFilterQuery());
                }
                return data;
            }
        }

        public static void Reload()
        {
            var tmp = new HisBhytParamGet().Get(new HisBhytParamFilterQuery());
            data = tmp;
        }
    }
}
