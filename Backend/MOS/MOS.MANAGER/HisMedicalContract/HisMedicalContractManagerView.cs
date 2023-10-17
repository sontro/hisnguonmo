using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicalContract
{
    public partial class HisMedicalContractManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_MEDICAL_CONTRACT>> GetView(HisMedicalContractViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MEDICAL_CONTRACT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MEDICAL_CONTRACT> resultData = null;
                if (valid)
                {
                    resultData = new HisMedicalContractGet(param).GetView(filter);
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
