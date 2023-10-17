using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineBean
{
    public partial class HisMedicineBeanManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_MEDICINE_BEAN_2>> GetView(HisMedicineBeanView2FilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MEDICINE_BEAN_2>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MEDICINE_BEAN_2> resultData = null;
                if (valid)
                {
                    resultData = new HisMedicineBeanGet(param).GetView2(filter);
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
