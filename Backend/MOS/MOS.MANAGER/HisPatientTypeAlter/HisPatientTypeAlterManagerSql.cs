using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPatientTypeAlter
{
    public partial class HisPatientTypeAlterManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<D_HIS_PATIENT_TYPE_ALTER_1>> GetDHisPatientTypeAlter1(DHisPatientTypeAlter1Filter filter)
        {
            ApiResultObject<List<D_HIS_PATIENT_TYPE_ALTER_1>> result = null;

            try
            {
                List<D_HIS_PATIENT_TYPE_ALTER_1> resultData = null;
                resultData = new HisPatientTypeAlterGet(param).GetDHisPatientTypeAlter1(filter);
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
