using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.Config
{
    class CosCFG
    {
        internal static string COS_FSS_BASE_URI = ConfigurationSettings.AppSettings["Onelink.Fss.Base.Uri"];

        private const string COS_REGISTER_CODE_CREATE = "MOS.SYNC_COS.REGISTER_CODE.CREATE";
        private static bool? isCreateRegisterCode;
        public static bool IS_CREATE_REGISTER_CODE
        {
            get
            {
                if (!isCreateRegisterCode.HasValue)
                {
                    isCreateRegisterCode = ConfigUtil.GetIntConfig(COS_REGISTER_CODE_CREATE) == 1;
                }

                return isCreateRegisterCode.Value;
            }
        }

        public static void Reload()
        {
            isCreateRegisterCode = ConfigUtil.GetIntConfig(COS_REGISTER_CODE_CREATE) == 1;
        }
    }
}
