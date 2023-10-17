using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMrCheckSummary
{
    partial class HisMrCheckSummaryDelete : BusinessBase
    {
        internal HisMrCheckSummaryDelete()
            : base()
        {

        }

        internal HisMrCheckSummaryDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_MR_CHECK_SUMMARY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMrCheckSummaryCheck checker = new HisMrCheckSummaryCheck(param);
                valid = valid && IsNotNull(data);
                HIS_MR_CHECK_SUMMARY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisMrCheckSummaryDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_MR_CHECK_SUMMARY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMrCheckSummaryCheck checker = new HisMrCheckSummaryCheck(param);
                List<HIS_MR_CHECK_SUMMARY> listRaw = new List<HIS_MR_CHECK_SUMMARY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisMrCheckSummaryDAO.DeleteList(listData);
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
