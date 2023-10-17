using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPosition
{
    public partial class HisPositionManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_POSITION>> GetView(HisPositionViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_POSITION>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_POSITION> resultData = null;
                if (valid)
                {
                    resultData = new HisPositionGet(param).GetView(filter);
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
