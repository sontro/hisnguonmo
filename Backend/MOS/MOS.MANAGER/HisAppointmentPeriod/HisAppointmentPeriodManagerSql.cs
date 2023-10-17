using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAppointmentPeriod
{
    public partial class HisAppointmentPeriodManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<HisAppointmentPeriodCountByDateSDO>> GetCountByDate(HisAppointmentPeriodCountByDateFilter filter)
        {
            ApiResultObject<List<HisAppointmentPeriodCountByDateSDO>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HisAppointmentPeriodCountByDateSDO> resultData = null;
                if (valid)
                {
                    resultData = new HisAppointmentPeriodGet(param).GetCountByDate(filter);
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
