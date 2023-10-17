using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDepositReason
{
    partial class HisDepositReasonCreate : BusinessBase
    {
		private List<HIS_DEPOSIT_REASON> recentHisDepositReasons = new List<HIS_DEPOSIT_REASON>();
		
        internal HisDepositReasonCreate()
            : base()
        {

        }

        internal HisDepositReasonCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_DEPOSIT_REASON data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDepositReasonCheck checker = new HisDepositReasonCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.DEPOSIT_REASON_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisDepositReasonDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDepositReason_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisDepositReason that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisDepositReasons.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisDepositReasons))
            {
                if (!DAOWorker.HisDepositReasonDAO.TruncateList(this.recentHisDepositReasons))
                {
                    LogSystem.Warn("Rollback du lieu HisDepositReason that bai, can kiem tra lai." + LogUtil.TraceData("recentHisDepositReasons", this.recentHisDepositReasons));
                }
				this.recentHisDepositReasons = null;
            }
        }
    }
}
