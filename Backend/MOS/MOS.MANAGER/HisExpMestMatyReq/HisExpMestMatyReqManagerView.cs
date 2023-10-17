using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestMatyReq
{
    public partial class HisExpMestMatyReqManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_EXP_MEST_MATY_REQ>> GetView(HisExpMestMatyReqViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_EXP_MEST_MATY_REQ>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_EXP_MEST_MATY_REQ> resultData = null;
                if (valid)
                {
                    resultData = new HisExpMestMatyReqGet(param).GetView(filter);
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
