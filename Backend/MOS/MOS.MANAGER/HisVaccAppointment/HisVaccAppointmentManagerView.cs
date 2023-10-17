using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccAppointment
{
    public partial class HisVaccAppointmentManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_VACC_APPOINTMENT>> GetView(HisVaccAppointmentViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_VACC_APPOINTMENT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_VACC_APPOINTMENT> resultData = null;
                if (valid)
                {
                    resultData = new HisVaccAppointmentGet(param).GetView(filter);
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
