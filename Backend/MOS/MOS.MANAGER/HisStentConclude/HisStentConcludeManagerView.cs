using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisStentConclude
{
    public partial class HisStentConcludeManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_STENT_CONCLUDE>> GetView(HisStentConcludeViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_STENT_CONCLUDE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_STENT_CONCLUDE> resultData = null;
                if (valid)
                {
                    resultData = new HisStentConcludeGet(param).GetView(filter);
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
