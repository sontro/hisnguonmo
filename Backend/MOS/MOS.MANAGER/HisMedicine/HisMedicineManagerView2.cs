using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicine
{
    public partial class HisMedicineManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_MEDICINE_2>> GetView2(HisMedicineView2FilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MEDICINE_2>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MEDICINE_2> resultData = null;
                if (valid)
                {
                    resultData = new HisMedicineGet(param).GetView2(filter);
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
