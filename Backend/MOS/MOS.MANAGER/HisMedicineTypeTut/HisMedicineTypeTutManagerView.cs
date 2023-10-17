using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineTypeTut
{
    public partial class HisMedicineTypeTutManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_MEDICINE_TYPE_TUT>> GetView(HisMedicineTypeTutViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MEDICINE_TYPE_TUT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MEDICINE_TYPE_TUT> resultData = null;
                if (valid)
                {
                    resultData = new HisMedicineTypeTutGet(param).GetView(filter);
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
