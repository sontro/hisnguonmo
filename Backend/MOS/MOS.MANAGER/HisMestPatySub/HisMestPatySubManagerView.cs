using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestPatySub
{
    public partial class HisMestPatySubManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_MEST_PATY_SUB>> GetView(HisMestPatySubViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MEST_PATY_SUB>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MEST_PATY_SUB> resultData = null;
                if (valid)
                {
                    resultData = new HisMestPatySubGet(param).GetView(filter);
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
