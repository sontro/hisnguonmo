using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskGeneral
{
    public partial class HisKskGeneralManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_KSK_GENERAL>> GetView(HisKskGeneralViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_KSK_GENERAL>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_KSK_GENERAL> resultData = null;
                if (valid)
                {
                    resultData = new HisKskGeneralGet(param).GetView(filter);
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
