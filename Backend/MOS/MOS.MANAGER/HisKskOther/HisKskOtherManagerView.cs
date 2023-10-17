using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskOther
{
    public partial class HisKskOtherManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_KSK_OTHER>> GetView(HisKskOtherViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_KSK_OTHER>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_KSK_OTHER> resultData = null;
                if (valid)
                {
                    resultData = new HisKskOtherGet(param).GetView(filter);
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
