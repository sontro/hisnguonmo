using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisEmployeeSchedule
{
    partial class HisEmployeeScheduleDelete : BusinessBase
    {
        internal HisEmployeeScheduleDelete()
            : base()
        {

        }

        internal HisEmployeeScheduleDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_EMPLOYEE_SCHEDULE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEmployeeScheduleCheck checker = new HisEmployeeScheduleCheck(param);
                valid = valid && IsNotNull(data);
                HIS_EMPLOYEE_SCHEDULE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisEmployeeScheduleDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_EMPLOYEE_SCHEDULE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisEmployeeScheduleCheck checker = new HisEmployeeScheduleCheck(param);
                List<HIS_EMPLOYEE_SCHEDULE> listRaw = new List<HIS_EMPLOYEE_SCHEDULE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisEmployeeScheduleDAO.DeleteList(listData);
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
