using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDeathWithin
{
    class HisDeathWithinUpdate : BusinessBase
    {
        internal HisDeathWithinUpdate()
            : base()
        {

        }

        internal HisDeathWithinUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_DEATH_WITHIN data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDeathWithinCheck checker = new HisDeathWithinCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                valid = valid && checker.ExistsCode(data.DEATH_WITHIN_CODE, data.ID);
                if (valid)
                {
                    result = DAOWorker.HisDeathWithinDAO.Update(data);
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

        internal bool UpdateList(List<HIS_DEATH_WITHIN> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDeathWithinCheck checker = new HisDeathWithinCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                    valid = valid && checker.ExistsCode(data.DEATH_WITHIN_CODE, data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisDeathWithinDAO.UpdateList(listData);
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
