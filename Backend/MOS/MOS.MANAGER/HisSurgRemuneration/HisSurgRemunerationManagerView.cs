using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSurgRemuneration
{
    public partial class HisSurgRemunerationManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_SURG_REMUNERATION>> GetView(HisSurgRemunerationViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_SURG_REMUNERATION>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_SURG_REMUNERATION> resultData = null;
                if (valid)
                {
                    resultData = new HisSurgRemunerationGet(param).GetView(filter);
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
