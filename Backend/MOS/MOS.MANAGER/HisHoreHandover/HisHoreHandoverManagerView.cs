using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHoreHandover
{
    public partial class HisHoreHandoverManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_HORE_HANDOVER>> GetView(HisHoreHandoverViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_HORE_HANDOVER>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_HORE_HANDOVER> resultData = null;
                if (valid)
                {
                    resultData = new HisHoreHandoverGet(param).GetView(filter);
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
