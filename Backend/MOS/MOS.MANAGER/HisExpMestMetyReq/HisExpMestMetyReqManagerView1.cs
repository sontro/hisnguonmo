using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestMetyReq
{
    public partial class HisExpMestMetyReqManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_EXP_MEST_METY_REQ_1>> GetView1(HisExpMestMetyReqView1FilterQuery filter)
        {
            ApiResultObject<List<V_HIS_EXP_MEST_METY_REQ_1>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_EXP_MEST_METY_REQ_1> resultData = null;
                if (valid)
                {
                    resultData = new HisExpMestMetyReqGet(param).GetView1(filter);
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
