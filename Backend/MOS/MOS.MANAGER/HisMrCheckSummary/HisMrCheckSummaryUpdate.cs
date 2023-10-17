using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMrCheckSummary
{
    partial class HisMrCheckSummaryUpdate : BusinessBase
    {
		private List<HIS_MR_CHECK_SUMMARY> beforeUpdateHisMrCheckSummarys = new List<HIS_MR_CHECK_SUMMARY>();
		
        internal HisMrCheckSummaryUpdate()
            : base()
        {

        }

        internal HisMrCheckSummaryUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MR_CHECK_SUMMARY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMrCheckSummaryCheck checker = new HisMrCheckSummaryCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_MR_CHECK_SUMMARY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisMrCheckSummaryDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMrCheckSummary_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMrCheckSummary that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisMrCheckSummarys.Add(raw);
                    result = true;
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

        internal bool UpdateList(List<HIS_MR_CHECK_SUMMARY> listData)
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
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisMrCheckSummaryDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMrCheckSummary_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMrCheckSummary that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisMrCheckSummarys.AddRange(listRaw);
                    result = true;
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
		
		internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.beforeUpdateHisMrCheckSummarys))
            {
                if (!DAOWorker.HisMrCheckSummaryDAO.UpdateList(this.beforeUpdateHisMrCheckSummarys))
                {
                    LogSystem.Warn("Rollback du lieu HisMrCheckSummary that bai, can kiem tra lai." + LogUtil.TraceData("HisMrCheckSummarys", this.beforeUpdateHisMrCheckSummarys));
                }
				this.beforeUpdateHisMrCheckSummarys = null;
            }
        }
    }
}
