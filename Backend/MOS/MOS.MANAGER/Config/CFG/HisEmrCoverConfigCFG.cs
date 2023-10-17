using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisEmrCoverConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.Config.CFG
{
    class HisEmrCoverConfigCFG
    {
        private static List<HIS_EMR_COVER_CONFIG> data;
        public static List<HIS_EMR_COVER_CONFIG> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisEmrCoverConfigGet().Get(new HisEmrCoverConfigFilterQuery());
                }
                return data;
            }
        }

        public static void Reload()
        {
            var tmp = new HisEmrCoverConfigGet().Get(new HisEmrCoverConfigFilterQuery());
            data = tmp;
        }
    }
}
