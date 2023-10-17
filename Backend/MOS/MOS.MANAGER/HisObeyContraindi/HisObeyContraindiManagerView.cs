using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisObeyContraindi
{
    public partial class HisObeyContraindiManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_OBEY_CONTRAINDI>> GetView(HisObeyContraindiViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_OBEY_CONTRAINDI>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_OBEY_CONTRAINDI> resultData = null;
                if (valid)
                {
                    resultData = new HisObeyContraindiGet(param).GetView(filter);
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
