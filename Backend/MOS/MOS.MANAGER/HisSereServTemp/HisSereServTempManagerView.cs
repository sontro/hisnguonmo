using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServTemp
{
    public partial class HisSereServTempManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_SERE_SERV_TEMP>> GetView(HisSereServTempViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_SERE_SERV_TEMP>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_SERE_SERV_TEMP> resultData = null;
                if (valid)
                {
                    resultData = new HisSereServTempGet(param).GetView(filter);
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
