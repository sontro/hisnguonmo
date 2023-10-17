using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAntigen
{
    public partial class HisAntigenManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_ANTIGEN>> GetView(HisAntigenViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_ANTIGEN>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_ANTIGEN> resultData = null;
                if (valid)
                {
                    resultData = new HisAntigenGet(param).GetView(filter);
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
