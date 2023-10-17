using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDepositReq
{
    partial class HisDepositReqCreate : BusinessBase
    {
		private List<HIS_DEPOSIT_REQ> recentHisDepositReqs = new List<HIS_DEPOSIT_REQ>();
		
        internal HisDepositReqCreate()
            : base()
        {

        }

        internal HisDepositReqCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_DEPOSIT_REQ data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDepositReqCheck checker = new HisDepositReqCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisDepositReqDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDepositReq_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisDepositReq that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisDepositReqs.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisDepositReqs))
            {
                if (!new HisDepositReqTruncate(param).TruncateList(this.recentHisDepositReqs))
                {
                    LogSystem.Warn("Rollback du lieu HisDepositReq that bai, can kiem tra lai." + LogUtil.TraceData("recentHisDepositReqs", this.recentHisDepositReqs));
                }
            }
        }
    }
}
