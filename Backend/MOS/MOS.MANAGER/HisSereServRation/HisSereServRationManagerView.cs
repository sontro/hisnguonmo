using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServRation
{
    public partial class HisSereServRationManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_SERE_SERV_RATION>> GetView(HisSereServRationViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_SERE_SERV_RATION>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_SERE_SERV_RATION> resultData = null;
                if (valid)
                {
                    resultData = new HisSereServRationGet(param).GetView(filter);
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
