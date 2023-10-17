using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRegimenHiv
{
    public partial class HisRegimenHivManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_REGIMEN_HIV>> GetView(HisRegimenHivViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_REGIMEN_HIV>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_REGIMEN_HIV> resultData = null;
                if (valid)
                {
                    resultData = new HisRegimenHivGet(param).GetView(filter);
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
