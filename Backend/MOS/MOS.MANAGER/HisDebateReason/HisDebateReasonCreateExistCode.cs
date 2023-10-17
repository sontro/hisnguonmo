using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDebateReason
{
    partial class HisDebateReasonCreate : BusinessBase
    {
		private List<HIS_DEBATE_REASON> recentHisDebateReasons = new List<HIS_DEBATE_REASON>();
		
        internal HisDebateReasonCreate()
            : base()
        {

        }

        internal HisDebateReasonCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_DEBATE_REASON data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDebateReasonCheck checker = new HisDebateReasonCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.DEBATE_REASON_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisDebateReasonDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDebateReason_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisDebateReason that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisDebateReasons.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisDebateReasons))
            {
                if (!DAOWorker.HisDebateReasonDAO.TruncateList(this.recentHisDebateReasons))
                {
                    LogSystem.Warn("Rollback du lieu HisDebateReason that bai, can kiem tra lai." + LogUtil.TraceData("recentHisDebateReasons", this.recentHisDebateReasons));
                }
				this.recentHisDebateReasons = null;
            }
        }
    }
}
