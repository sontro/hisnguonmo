using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMestPeriodBlood
{
    partial class HisMestPeriodBloodDelete : BusinessBase
    {
        internal HisMestPeriodBloodDelete()
            : base()
        {

        }

        internal HisMestPeriodBloodDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_MEST_PERIOD_BLOOD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMestPeriodBloodCheck checker = new HisMestPeriodBloodCheck(param);
                valid = valid && IsNotNull(data);
                HIS_MEST_PERIOD_BLOOD raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisMestPeriodBloodDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_MEST_PERIOD_BLOOD> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMestPeriodBloodCheck checker = new HisMestPeriodBloodCheck(param);
                List<HIS_MEST_PERIOD_BLOOD> listRaw = new List<HIS_MEST_PERIOD_BLOOD>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisMestPeriodBloodDAO.DeleteList(listData);
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
