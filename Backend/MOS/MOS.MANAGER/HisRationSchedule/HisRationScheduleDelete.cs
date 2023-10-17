using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisRationSchedule
{
    partial class HisRationScheduleDelete : BusinessBase
    {
        internal HisRationScheduleDelete()
            : base()
        {

        }

        internal HisRationScheduleDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_RATION_SCHEDULE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRationScheduleCheck checker = new HisRationScheduleCheck(param);
                valid = valid && IsNotNull(data);
                HIS_RATION_SCHEDULE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisRationScheduleDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_RATION_SCHEDULE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRationScheduleCheck checker = new HisRationScheduleCheck(param);
                List<HIS_RATION_SCHEDULE> listRaw = new List<HIS_RATION_SCHEDULE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisRationScheduleDAO.DeleteList(listData);
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
