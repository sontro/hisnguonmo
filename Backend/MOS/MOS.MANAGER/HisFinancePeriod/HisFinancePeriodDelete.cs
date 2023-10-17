using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisFinancePeriod
{
    partial class HisFinancePeriodDelete : BusinessBase
    {
        internal HisFinancePeriodDelete()
            : base()
        {

        }

        internal HisFinancePeriodDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_FINANCE_PERIOD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisFinancePeriodCheck checker = new HisFinancePeriodCheck(param);
                valid = valid && IsNotNull(data);
                HIS_FINANCE_PERIOD raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisFinancePeriodDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_FINANCE_PERIOD> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisFinancePeriodCheck checker = new HisFinancePeriodCheck(param);
                List<HIS_FINANCE_PERIOD> listRaw = new List<HIS_FINANCE_PERIOD>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisFinancePeriodDAO.DeleteList(listData);
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
