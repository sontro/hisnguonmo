using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPatientTypeRoom
{
    public partial class HisPatientTypeRoomManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_PATIENT_TYPE_ROOM>> GetView(HisPatientTypeRoomViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_PATIENT_TYPE_ROOM>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_PATIENT_TYPE_ROOM> resultData = null;
                if (valid)
                {
                    resultData = new HisPatientTypeRoomGet(param).GetView(filter);
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
