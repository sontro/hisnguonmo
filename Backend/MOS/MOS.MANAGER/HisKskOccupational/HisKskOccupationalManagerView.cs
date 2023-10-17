using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskOccupational
{
    public partial class HisKskOccupationalManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_KSK_OCCUPATIONAL>> GetView(HisKskOccupationalViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_KSK_OCCUPATIONAL>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_KSK_OCCUPATIONAL> resultData = null;
                if (valid)
                {
                    resultData = new HisKskOccupationalGet(param).GetView(filter);
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
