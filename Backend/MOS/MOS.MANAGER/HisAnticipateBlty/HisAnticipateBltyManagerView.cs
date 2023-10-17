using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAnticipateBlty
{
    public partial class HisAnticipateBltyManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_ANTICIPATE_BLTY>> GetView(HisAnticipateBltyViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_ANTICIPATE_BLTY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_ANTICIPATE_BLTY> resultData = null;
                if (valid)
                {
                    resultData = new HisAnticipateBltyGet(param).GetView(filter);
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
