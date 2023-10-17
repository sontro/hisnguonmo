using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVitaminA
{
    public partial class HisVitaminAManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_VITAMIN_A>> GetView(HisVitaminAViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_VITAMIN_A>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_VITAMIN_A> resultData = null;
                if (valid)
                {
                    resultData = new HisVitaminAGet(param).GetView(filter);
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
