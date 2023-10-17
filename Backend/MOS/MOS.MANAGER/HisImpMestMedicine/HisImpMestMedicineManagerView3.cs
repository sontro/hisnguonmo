using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMestMedicine
{
    public partial class HisImpMestMedicineManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_IMP_MEST_MEDICINE_3>> GetView3(HisImpMestMedicineView3FilterQuery filter)
        {
            ApiResultObject<List<V_HIS_IMP_MEST_MEDICINE_3>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_IMP_MEST_MEDICINE_3> resultData = null;
                if (valid)
                {
                    resultData = new HisImpMestMedicineGet(param).GetView3(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }
    }
}
