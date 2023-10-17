using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisKskPeriodDriver
{
    partial class HisKskPeriodDriverDelete : BusinessBase
    {
        internal HisKskPeriodDriverDelete()
            : base()
        {

        }

        internal HisKskPeriodDriverDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_KSK_PERIOD_DRIVER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisKskPeriodDriverCheck checker = new HisKskPeriodDriverCheck(param);
                valid = valid && IsNotNull(data);
                HIS_KSK_PERIOD_DRIVER raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisKskPeriodDriverDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_KSK_PERIOD_DRIVER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisKskPeriodDriverCheck checker = new HisKskPeriodDriverCheck(param);
                List<HIS_KSK_PERIOD_DRIVER> listRaw = new List<HIS_KSK_PERIOD_DRIVER>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisKskPeriodDriverDAO.DeleteList(listData);
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
