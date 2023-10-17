using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHoreHoha
{
    public partial class HisHoreHohaManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_HORE_HOHA>> GetView(HisHoreHohaViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_HORE_HOHA>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_HORE_HOHA> resultData = null;
                if (valid)
                {
                    resultData = new HisHoreHohaGet(param).GetView(filter);
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
