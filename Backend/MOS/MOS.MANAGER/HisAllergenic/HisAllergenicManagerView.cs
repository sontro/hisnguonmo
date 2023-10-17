using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAllergenic
{
    public partial class HisAllergenicManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_ALLERGENIC>> GetView(HisAllergenicViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_ALLERGENIC>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_ALLERGENIC> resultData = null;
                if (valid)
                {
                    resultData = new HisAllergenicGet(param).GetView(filter);
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
