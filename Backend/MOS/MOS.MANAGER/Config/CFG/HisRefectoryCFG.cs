using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisRefectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.Config
{
    class HisRefectoryCFG
    {
        private static List<V_HIS_REFECTORY> data;
        public static List<V_HIS_REFECTORY> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisRefectoryGet().GetView(new HisRefectoryViewFilterQuery());
                }
                return data;
            }
        }

        public static void Reload()
        {
            data = new HisRefectoryGet().GetView(new HisRefectoryViewFilterQuery());
        }
    }
}
