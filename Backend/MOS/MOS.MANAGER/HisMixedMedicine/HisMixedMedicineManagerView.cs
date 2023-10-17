using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMixedMedicine
{
    public partial class HisMixedMedicineManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_MIXED_MEDICINE>> GetView(HisMixedMedicineViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MIXED_MEDICINE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MIXED_MEDICINE> resultData = null;
                if (valid)
                {
                    resultData = new HisMixedMedicineGet(param).GetView(filter);
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
