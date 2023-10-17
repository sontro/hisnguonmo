using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestPeriodMaty
{
    class HisMestPeriodMatyTruncate : BusinessBase
    {
        internal HisMestPeriodMatyTruncate()
            : base()
        {

        }

        internal HisMestPeriodMatyTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_MEST_PERIOD_MATY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMestPeriodMatyCheck checker = new HisMestPeriodMatyCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisMestPeriodMatyDAO.Truncate(data);
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

        internal bool TruncateList(List<HIS_MEST_PERIOD_MATY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMestPeriodMatyCheck checker = new HisMestPeriodMatyCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisMestPeriodMatyDAO.TruncateList(listData);
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

        internal bool TruncateByMediStockPeriodId(long id)
        {
            bool result = false;
            try
            {
                List<HIS_MEST_PERIOD_MATY> list = new HisMestPeriodMatyGet().GetByMediStockPeriodId(id);
                if (IsNotNullOrEmpty(list))
                {
                    result = this.TruncateList(list);
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
