using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRestRetrType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMrChecklist
{
    partial class HisMrChecklistTruncate : BusinessBase
    {
        internal HisMrChecklistTruncate()
            : base()
        {

        }

        internal HisMrChecklistTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMrChecklistCheck checker = new HisMrChecklistCheck(param);
                HIS_MR_CHECKLIST raw = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(id);
                if (valid)
                {
                    result = DAOWorker.HisMrChecklistDAO.Truncate(raw);
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

        internal bool TruncateList(List<HIS_MR_CHECKLIST> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMrChecklistCheck checker = new HisMrChecklistCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
					valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisMrChecklistDAO.TruncateList(listData);
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

        internal bool TruncateByMrCheckSummaryId(long mrCheckSummaryId)
        {
            bool result = false;
            try
            {
                List<HIS_MR_CHECKLIST> mrChecklists = new HisMrChecklistGet().GetByMrCheckSummaryId(mrCheckSummaryId);
                if (IsNotNullOrEmpty(mrChecklists))
                {
                    result = this.TruncateList(mrChecklists);
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

        internal bool TruncateByMrCheckSummaryIds(List<long> mrCheckSummaryIds)
        {
            bool result = false;
            try
            {
                List<HIS_MR_CHECKLIST> mrChecklists = new HisMrChecklistGet().GetByMrCheckSummaryIds(mrCheckSummaryIds);
                if (IsNotNullOrEmpty(mrChecklists))
                {
                    result = this.TruncateList(mrChecklists);
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
