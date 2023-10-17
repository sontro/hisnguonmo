using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisWelfareType
{
    public partial class HisWelfareTypeManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_WELFARE_TYPE>> GetView(HisWelfareTypeViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_WELFARE_TYPE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_WELFARE_TYPE> resultData = null;
                if (valid)
                {
                    resultData = new HisWelfareTypeGet(param).GetView(filter);
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
