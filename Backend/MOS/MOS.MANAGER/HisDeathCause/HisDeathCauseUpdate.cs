using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDeathCause
{
    class HisDeathCauseUpdate : BusinessBase
    {
        internal HisDeathCauseUpdate()
            : base()
        {

        }

        internal HisDeathCauseUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_DEATH_CAUSE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDeathCauseCheck checker = new HisDeathCauseCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                valid = valid && checker.ExistsCode(data.DEATH_CAUSE_CODE, data.ID);
                if (valid)
                {
                    result = DAOWorker.HisDeathCauseDAO.Update(data);
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

        internal bool UpdateList(List<HIS_DEATH_CAUSE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDeathCauseCheck checker = new HisDeathCauseCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                    valid = valid && checker.ExistsCode(data.DEATH_CAUSE_CODE, data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisDeathCauseDAO.UpdateList(listData);
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
