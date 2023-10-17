using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisExecuteRole;
using MOS.MANAGER.HisMestRoom;
using MOS.MANAGER.HisPatientType;
using MOS.MANAGER.HisPatientTypeAllow;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.Config
{
    public class HisExecuteRoleCFG
    {
        private const string EXECUTE_ROLE_CODE__MAIN_CFG = "DBCODE.HIS_RS.HIS_EXECUTE_ROLE.EXECUTE_ROLE_CODE.MAIN";
        private const string EXECUTE_ROLE_CODE__ANESTHETIST_CFG = "DBCODE.HIS_RS.HIS_EXECUTE_ROLE.EXECUTE_ROLE_CODE.ANESTHETIST";

        private static List<HIS_EXECUTE_ROLE> data;
        public static List<HIS_EXECUTE_ROLE> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisExecuteRoleGet().Get(new HisExecuteRoleFilterQuery());
                }
                return data;
            }
        }

        private static string executeRoleCodeMain;
        public static string EXECUTE_ROLE_CODE__MAIN
        {
            get
            {
                if (executeRoleCodeMain == null)
                {
                    executeRoleCodeMain = ConfigUtil.GetStrConfig(EXECUTE_ROLE_CODE__MAIN_CFG);
                }
                return executeRoleCodeMain;
            }
            set
            {
                executeRoleCodeMain = value;
            }
        }

        private static string executeRoleCodeAnesthetist;
        public static string EXECUTE_ROLE_CODE__ANESTHETIST
        {
            get
            {
                if (executeRoleCodeAnesthetist == null)
                {
                    executeRoleCodeAnesthetist = ConfigUtil.GetStrConfig(EXECUTE_ROLE_CODE__ANESTHETIST_CFG);
                }
                return executeRoleCodeAnesthetist;
            }
            set
            {
                executeRoleCodeAnesthetist = value;
            }
        }

        public static void Reload()
        {
            var tmp = new HisExecuteRoleGet().Get(new HisExecuteRoleFilterQuery());
            data = tmp;
            executeRoleCodeMain = ConfigUtil.GetStrConfig(EXECUTE_ROLE_CODE__MAIN_CFG);
            executeRoleCodeAnesthetist = ConfigUtil.GetStrConfig(EXECUTE_ROLE_CODE__ANESTHETIST_CFG);
        }
    }
}
