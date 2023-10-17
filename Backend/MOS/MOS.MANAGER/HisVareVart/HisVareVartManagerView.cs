using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVareVart
{
    public partial class HisVareVartManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_VARE_VART>> GetView(HisVareVartViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_VARE_VART>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_VARE_VART> resultData = null;
                if (valid)
                {
                    resultData = new HisVareVartGet(param).GetView(filter);
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
