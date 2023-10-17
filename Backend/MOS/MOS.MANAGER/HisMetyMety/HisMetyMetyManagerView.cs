using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMetyMety
{
    public partial class HisMetyMetyManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_METY_METY>> GetView(HisMetyMetyViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_METY_METY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_METY_METY> resultData = null;
                if (valid)
                {
                    resultData = new HisMetyMetyGet(param).GetView(filter);
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
