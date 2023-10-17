using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisBranch;
using MOS.MANAGER.HisPatientType;
using MOS.MANAGER.HisPatientTypeAllow;
using MOS.MANAGER.HisService;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.Config
{
    class HisBranchCFG
    {
        private static string SYNC_HID = "CONFIG_KEY__IS_USE_HID_SYNC";
        /// <summary>
        /// Du lieu cua cac chi nhanh co doc lap hay khong
        /// </summary>
        private const string IS_ISOLATION_CFG = "MOS.HIS_BRANCH.IS_ISOLATION";

        private const string CREATE_ROLE_DEFAULT_CFG = "MOS.ACS_USER.AUTO_CREATE.ROLE_DEFAULT";

        private static List<HIS_BRANCH> data;
        public static List<HIS_BRANCH> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisBranchGet().Get(new HisBranchFilterQuery());
                }
                return data;
            }
        }

        private static bool? isSyncHid;
        public static bool IS_SYNC_HID
        {
            get
            {
                if (!isSyncHid.HasValue)
                {
                    isSyncHid = ConfigUtil.GetIntConfig(SYNC_HID) == 1;
                }
                return isSyncHid.Value;
            }
        }

        private static bool? isIsolation;
        public static bool IS_ISOLATION
        {
            get
            {
                if (!isIsolation.HasValue)
                {
                    isIsolation = ConfigUtil.GetIntConfig(IS_ISOLATION_CFG) == 1;
                }
                return isIsolation.Value;
            }
            set
            {
                isIsolation = value;
            }
        }

        private static string acsUserRoleDefault;
        public static string ACS_USER_ROLE
        {
            get
            {
                if (acsUserRoleDefault == null)
                {
                    acsUserRoleDefault = ConfigUtil.GetStrConfig(CREATE_ROLE_DEFAULT_CFG);
                }
                return acsUserRoleDefault;
            }
            set
            {
                acsUserRoleDefault = value;
            }
        }

        public static void Reload()
        {
            var tmp = new HisBranchGet().Get(new HisBranchFilterQuery());
            data = tmp;
            isSyncHid = ConfigUtil.GetIntConfig(SYNC_HID) == 1;
            isIsolation = ConfigUtil.GetIntConfig(IS_ISOLATION_CFG) == 1;
            acsUserRoleDefault = ConfigUtil.GetStrConfig(CREATE_ROLE_DEFAULT_CFG);
        }
    }
}
