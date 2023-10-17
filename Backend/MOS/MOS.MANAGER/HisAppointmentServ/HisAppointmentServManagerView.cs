using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAppointmentServ
{
    public partial class HisAppointmentServManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_APPOINTMENT_SERV>> GetView(HisAppointmentServViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_APPOINTMENT_SERV>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_APPOINTMENT_SERV> resultData = null;
                if (valid)
                {
                    resultData = new HisAppointmentServGet(param).GetView(filter);
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
