using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskAccess
{
    public partial class HisKskAccessManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_KSK_ACCESS>> GetView(HisKskAccessViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_KSK_ACCESS>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_KSK_ACCESS> resultData = null;
                if (valid)
                {
                    resultData = new HisKskAccessGet(param).GetView(filter);
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
