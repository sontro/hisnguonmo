using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisExroRoom;
using MOS.MANAGER.HisOtherPaySource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.Config
{
    class HisOtherPaySourceCFG
    {
        private static List<HIS_OTHER_PAY_SOURCE> data;
        public static List<HIS_OTHER_PAY_SOURCE> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisOtherPaySourceGet().Get(new HisOtherPaySourceFilterQuery());
                }
                return data;
            }
        }

        public static void Reload()
        {
            var tmp = new HisOtherPaySourceGet().Get(new HisOtherPaySourceFilterQuery());
            data = tmp;
        }
    }
}
