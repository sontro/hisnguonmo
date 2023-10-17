using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEmrCoverConfig
{
    public partial class HisEmrCoverConfigManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_EMR_COVER_CONFIG>> GetView(HisEmrCoverConfigViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_EMR_COVER_CONFIG>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_EMR_COVER_CONFIG> resultData = null;
                if (valid)
                {
                    resultData = new HisEmrCoverConfigGet(param).GetView(filter);
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
