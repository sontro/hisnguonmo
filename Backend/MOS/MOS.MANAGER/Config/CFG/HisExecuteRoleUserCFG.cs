
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisExecuteRoleUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.Config
{
    public class HisExecuteRoleUserCFG
    {
        private static List<HIS_EXECUTE_ROLE_USER> data;
        public static List<HIS_EXECUTE_ROLE_USER> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisExecuteRoleUserGet().Get(new HisExecuteRoleUserFilterQuery());
                }
                return data;
            }
        }

        public static void Reload()
        {
            var tmp = new HisExecuteRoleUserGet().Get(new HisExecuteRoleUserFilterQuery());
            data = tmp;
        }
    }
}
