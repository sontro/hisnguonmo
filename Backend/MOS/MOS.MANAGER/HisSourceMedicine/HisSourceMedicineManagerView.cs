using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSourceMedicine
{
    public partial class HisSourceMedicineManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_SOURCE_MEDICINE>> GetView(HisSourceMedicineViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_SOURCE_MEDICINE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_SOURCE_MEDICINE> resultData = null;
                if (valid)
                {
                    resultData = new HisSourceMedicineGet(param).GetView(filter);
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
