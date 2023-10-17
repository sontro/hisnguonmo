using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExeServiceModule
{
    public partial class HisExeServiceModuleManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_EXE_SERVICE_MODULE>> GetView(HisExeServiceModuleViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_EXE_SERVICE_MODULE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_EXE_SERVICE_MODULE> resultData = null;
                if (valid)
                {
                    resultData = new HisExeServiceModuleGet(param).GetView(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }
    }
}
