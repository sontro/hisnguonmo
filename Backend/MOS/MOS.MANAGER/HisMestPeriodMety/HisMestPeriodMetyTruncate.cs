using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestPeriodMety
{
    class HisMestPeriodMetyTruncate : BusinessBase
    {
        internal HisMestPeriodMetyTruncate()
            : base()
        {

        }

        internal HisMestPeriodMetyTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_MEST_PERIOD_METY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMestPeriodMetyCheck checker = new HisMestPeriodMetyCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisMestPeriodMetyDAO.Truncate(data);
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

        internal bool TruncateList(List<HIS_MEST_PERIOD_METY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMestPeriodMetyCheck checker = new HisMestPeriodMetyCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisMestPeriodMetyDAO.TruncateList(listData);
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
                List<HIS_MEST_PERIOD_METY> list = new HisMestPeriodMetyGet().GetByMediStockPeriodId(id);
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
