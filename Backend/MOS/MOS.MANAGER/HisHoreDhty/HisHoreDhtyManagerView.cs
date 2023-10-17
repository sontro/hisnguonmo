using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHoreDhty
{
    public partial class HisHoreDhtyManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_HORE_DHTY>> GetView(HisHoreDhtyViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_HORE_DHTY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_HORE_DHTY> resultData = null;
                if (valid)
                {
                    resultData = new HisHoreDhtyGet(param).GetView(filter);
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
