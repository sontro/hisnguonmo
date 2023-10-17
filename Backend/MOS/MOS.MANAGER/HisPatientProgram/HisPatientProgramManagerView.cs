using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPatientProgram
{
    public partial class HisPatientProgramManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_PATIENT_PROGRAM>> GetView(HisPatientProgramViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_PATIENT_PROGRAM>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_PATIENT_PROGRAM> resultData = null;
                if (valid)
                {
                    resultData = new HisPatientProgramGet(param).GetView(filter);
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

        [Logger]
        public ApiResultObject<V_HIS_PATIENT_PROGRAM> GetViewByCode(string code)
        {
            ApiResultObject<V_HIS_PATIENT_PROGRAM> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(code);
                V_HIS_PATIENT_PROGRAM resultData = null;
                if (valid)
                {
                    resultData = new HisPatientProgramGet(param).GetViewByCode(code);
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
