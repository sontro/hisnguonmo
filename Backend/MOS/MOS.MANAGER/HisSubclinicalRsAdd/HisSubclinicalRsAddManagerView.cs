using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSubclinicalRsAdd
{
    public partial class HisSubclinicalRsAddManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_SUBCLINICAL_RS_ADD>> GetView(HisSubclinicalRsAddViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_SUBCLINICAL_RS_ADD>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_SUBCLINICAL_RS_ADD> resultData = null;
                if (valid)
                {
                    resultData = new HisSubclinicalRsAddGet(param).GetView(filter);
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
