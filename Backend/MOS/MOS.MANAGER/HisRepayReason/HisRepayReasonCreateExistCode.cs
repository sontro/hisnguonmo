using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRepayReason
{
    partial class HisRepayReasonCreate : BusinessBase
    {
		private List<HIS_REPAY_REASON> recentHisRepayReasons = new List<HIS_REPAY_REASON>();
		
        internal HisRepayReasonCreate()
            : base()
        {

        }

        internal HisRepayReasonCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_REPAY_REASON data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRepayReasonCheck checker = new HisRepayReasonCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.REPAY_REASON_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisRepayReasonDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRepayReason_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisRepayReason that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisRepayReasons.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisRepayReasons))
            {
                if (!new HisRepayReasonTruncate(param).TruncateList(this.recentHisRepayReasons))
                {
                    LogSystem.Warn("Rollback du lieu HisRepayReason that bai, can kiem tra lai." + LogUtil.TraceData("recentHisRepayReasons", this.recentHisRepayReasons));
                }
            }
        }
    }
}
