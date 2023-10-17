using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEmergencyWtime
{
    class HisEmergencyWtimeUpdate : BusinessBase
    {
        internal HisEmergencyWtimeUpdate()
            : base()
        {

        }

        internal HisEmergencyWtimeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_EMERGENCY_WTIME data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEmergencyWtimeCheck checker = new HisEmergencyWtimeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                valid = valid && checker.ExistsCode(data.EMERGENCY_WTIME_CODE, data.ID);
                if (valid)
                {
                    result = DAOWorker.HisEmergencyWtimeDAO.Update(data);
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

        internal bool UpdateList(List<HIS_EMERGENCY_WTIME> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisEmergencyWtimeCheck checker = new HisEmergencyWtimeCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                    valid = valid && checker.ExistsCode(data.EMERGENCY_WTIME_CODE, data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisEmergencyWtimeDAO.UpdateList(listData);
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
