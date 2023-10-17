using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMestPeriodBlty
{
    partial class HisMestPeriodBltyDelete : BusinessBase
    {
        internal HisMestPeriodBltyDelete()
            : base()
        {

        }

        internal HisMestPeriodBltyDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_MEST_PERIOD_BLTY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMestPeriodBltyCheck checker = new HisMestPeriodBltyCheck(param);
                valid = valid && IsNotNull(data);
                HIS_MEST_PERIOD_BLTY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisMestPeriodBltyDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_MEST_PERIOD_BLTY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMestPeriodBltyCheck checker = new HisMestPeriodBltyCheck(param);
                List<HIS_MEST_PERIOD_BLTY> listRaw = new List<HIS_MEST_PERIOD_BLTY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisMestPeriodBltyDAO.DeleteList(listData);
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
