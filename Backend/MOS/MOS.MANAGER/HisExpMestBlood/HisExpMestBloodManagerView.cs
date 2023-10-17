using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestBlood
{
    public partial class HisExpMestBloodManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_EXP_MEST_BLOOD>> GetView(HisExpMestBloodViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_EXP_MEST_BLOOD>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_EXP_MEST_BLOOD> resultData = null;
                if (valid)
                {
                    resultData = new HisExpMestBloodGet(param).GetView(filter);
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
