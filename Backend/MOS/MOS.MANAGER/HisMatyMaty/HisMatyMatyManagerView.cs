using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMatyMaty
{
    public partial class HisMatyMatyManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_MATY_MATY>> GetView(HisMatyMatyViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MATY_MATY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MATY_MATY> resultData = null;
                if (valid)
                {
                    resultData = new HisMatyMatyGet(param).GetView(filter);
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
