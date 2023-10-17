using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMetyMaty
{
    public partial class HisMetyMatyManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_METY_MATY>> GetView(HisMetyMatyViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_METY_MATY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_METY_MATY> resultData = null;
                if (valid)
                {
                    resultData = new HisMetyMatyGet(param).GetView(filter);
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
