using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCaroDepartment
{
    public partial class HisCaroDepartmentManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_CARO_DEPARTMENT>> GetView(HisCaroDepartmentViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_CARO_DEPARTMENT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_CARO_DEPARTMENT> resultData = null;
                if (valid)
                {
                    resultData = new HisCaroDepartmentGet(param).GetView(filter);
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
