using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAppointmentPeriod
{
    partial class HisAppointmentPeriodDelete : BusinessBase
    {
        internal HisAppointmentPeriodDelete()
            : base()
        {

        }

        internal HisAppointmentPeriodDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_APPOINTMENT_PERIOD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAppointmentPeriodCheck checker = new HisAppointmentPeriodCheck(param);
                valid = valid && IsNotNull(data);
                HIS_APPOINTMENT_PERIOD raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisAppointmentPeriodDAO.Delete(data);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool DeleteList(List<HIS_APPOINTMENT_PERIOD> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAppointmentPeriodCheck checker = new HisAppointmentPeriodCheck(param);
                List<HIS_APPOINTMENT_PERIOD> listRaw = new List<HIS_APPOINTMENT_PERIOD>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisAppointmentPeriodDAO.DeleteList(listData);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
