using ACS.EFMODEL.DataModels;
using ACS.Filter;
using MOS.MANAGER.AcsUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.Config
{
    class AcsUserCFG
    {
        private static List<ACS_USER> data;
        public static List<ACS_USER> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new AcsUserGet().GetByWrapper(new AcsUserFilter());
                }
                return data;
            }
        }

        public static void Reload()
        {
            var tmp = new AcsUserGet().GetByWrapper(new AcsUserFilter());
            data = tmp;
        }
    }
}
