using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestPeriodMedi
{
    class HisMestPeriodMediTruncate : BusinessBase
    {
        internal HisMestPeriodMediTruncate()
            : base()
        {

        }

        internal HisMestPeriodMediTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_MEST_PERIOD_MEDI data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMestPeriodMediCheck checker = new HisMestPeriodMediCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisMestPeriodMediDAO.Truncate(data);
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

        internal bool TruncateList(List<HIS_MEST_PERIOD_MEDI> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMestPeriodMediCheck checker = new HisMestPeriodMediCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisMestPeriodMediDAO.TruncateList(listData);
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
                List<HIS_MEST_PERIOD_MEDI> list = new HisMestPeriodMediGet().GetByMediStockPeriodId(id);
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
