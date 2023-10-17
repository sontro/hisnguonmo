using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMrCheckSummary
{
    partial class HisMrCheckSummaryCreate : BusinessBase
    {
		private List<HIS_MR_CHECK_SUMMARY> recentHisMrCheckSummarys = new List<HIS_MR_CHECK_SUMMARY>();
		
        internal HisMrCheckSummaryCreate()
            : base()
        {

        }

        internal HisMrCheckSummaryCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MR_CHECK_SUMMARY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMrCheckSummaryCheck checker = new HisMrCheckSummaryCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.MR_CHECK_SUMMARY_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisMrCheckSummaryDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMrCheckSummary_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMrCheckSummary that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMrCheckSummarys.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisMrCheckSummarys))
            {
                if (!DAOWorker.HisMrCheckSummaryDAO.TruncateList(this.recentHisMrCheckSummarys))
                {
                    LogSystem.Warn("Rollback du lieu HisMrCheckSummary that bai, can kiem tra lai." + LogUtil.TraceData("recentHisMrCheckSummarys", this.recentHisMrCheckSummarys));
                }
				this.recentHisMrCheckSummarys = null;
            }
        }
    }
}
