using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestPatyTrty
{
    public partial class HisMestPatyTrtyManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_MEST_PATY_TRTY>> GetView(HisMestPatyTrtyViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MEST_PATY_TRTY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MEST_PATY_TRTY> resultData = null;
                if (valid)
                {
                    resultData = new HisMestPatyTrtyGet(param).GetView(filter);
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
