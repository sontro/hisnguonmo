using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTransfusion
{
    public partial class HisTransfusionManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_TRANSFUSION>> GetView(HisTransfusionViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_TRANSFUSION>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_TRANSFUSION> resultData = null;
                if (valid)
                {
                    resultData = new HisTransfusionGet(param).GetView(filter);
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
