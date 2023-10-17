using Inventec.Common.Logging;
using Inventec.Core;
using MOS.Filter;
using System;
using System.Linq;
using System.Collections.Generic;
using Inventec.Common.LocalStorage.SdaConfig;
using HIS.Desktop.LocalStorage.BackendData;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.Bordereau.Config
{
    public class HisExecuteRoleCFG
    {
        private const string KEY__EXECUTE_ROLE_CODE__MAIN = "DBCODE.HIS_RS.HIS_EXECUTE_ROLE.EXECUTE_ROLE_CODE.MAIN";

        private static long executeRoleCodeIdMain;
        public static long EXECUTE_ROLE_CODE__ID_MAIN
        {
            get
            {
                if (executeRoleCodeIdMain == 0)
                {
                    executeRoleCodeIdMain = GetId(HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(KEY__EXECUTE_ROLE_CODE__MAIN));
                }
                return executeRoleCodeIdMain;
            }
            set
            {
                executeRoleCodeIdMain = value;
            }
        }

        private static long GetId(string code)
        {
            long result = 0;
            try
            {
                var data = BackendDataWorker.Get<HIS_PATIENT_TYPE>().FirstOrDefault(o => o.PATIENT_TYPE_CODE == code);
                if (!(data != null && data.ID > 0)) throw new ArgumentNullException(code + LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => code), code));
                result = data.ID;
            }
            catch (Exception ex)
            {
                LogSystem.Debug(ex);
                result = 0;
            }
            return result;
        }
    }
}
