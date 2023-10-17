using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineService
{
    public partial class HisMedicineServiceManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_MEDICINE_SERVICE>> GetView(HisMedicineServiceViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MEDICINE_SERVICE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MEDICINE_SERVICE> resultData = null;
                if (valid)
                {
                    resultData = new HisMedicineServiceGet(param).GetView(filter);
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
