using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDosageForm
{
    public partial class HisDosageFormManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_DOSAGE_FORM>> GetView(HisDosageFormViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_DOSAGE_FORM>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_DOSAGE_FORM> resultData = null;
                if (valid)
                {
                    resultData = new HisDosageFormGet(param).GetView(filter);
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
