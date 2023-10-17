using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAnticipate
{
    public partial class HisAnticipateManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_ANTICIPATE>> GetView(HisAnticipateViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_ANTICIPATE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_ANTICIPATE> resultData = null;
                if (valid)
                {
                    resultData = new HisAnticipateGet(param).GetView(filter);
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
