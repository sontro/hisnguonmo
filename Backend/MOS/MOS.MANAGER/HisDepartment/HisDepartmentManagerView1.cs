using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.TDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDepartment
{
    public partial class HisDepartmentManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_DEPARTMENT_1>> GetView1(HisDepartmentView1FilterQuery filter)
        {
            ApiResultObject<List<V_HIS_DEPARTMENT_1>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_DEPARTMENT_1> resultData = null;
                if (valid)
                {
                    resultData = new HisDepartmentGet(param).GetView1(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }
    }
}
