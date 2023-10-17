using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTranPatiTemp
{
    public partial class HisTranPatiTempManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_TRAN_PATI_TEMP>> GetView(HisTranPatiTempViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_TRAN_PATI_TEMP>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_TRAN_PATI_TEMP> resultData = null;
                if (valid)
                {
                    resultData = new HisTranPatiTempGet(param).GetView(filter);
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
