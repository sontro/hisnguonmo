using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCancelReason
{
    partial class HisCancelReasonCreate : BusinessBase
    {
		private List<HIS_CANCEL_REASON> recentHisCancelReasons = new List<HIS_CANCEL_REASON>();
		
        internal HisCancelReasonCreate()
            : base()
        {

        }

        internal HisCancelReasonCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_CANCEL_REASON data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCancelReasonCheck checker = new HisCancelReasonCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.CANCEL_REASON_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisCancelReasonDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCancelReason_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisCancelReason that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisCancelReasons.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisCancelReasons))
            {
                if (!DAOWorker.HisCancelReasonDAO.TruncateList(this.recentHisCancelReasons))
                {
                    LogSystem.Warn("Rollback du lieu HisCancelReason that bai, can kiem tra lai." + LogUtil.TraceData("recentHisCancelReasons", this.recentHisCancelReasons));
                }
				this.recentHisCancelReasons = null;
            }
        }
    }
}
