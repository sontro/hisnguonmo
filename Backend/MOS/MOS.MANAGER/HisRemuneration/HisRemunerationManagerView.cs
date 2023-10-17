using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRemuneration
{
    public partial class HisRemunerationManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_REMUNERATION>> GetView(HisRemunerationViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_REMUNERATION>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_REMUNERATION> resultData = null;
                if (valid)
                {
                    resultData = new HisRemunerationGet(param).GetView(filter);
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
