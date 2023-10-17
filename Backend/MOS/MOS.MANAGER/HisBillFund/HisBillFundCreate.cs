using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBillFund
{
    partial class HisBillFundCreate : BusinessBase
    {
		private List<HIS_BILL_FUND> recentHisBillFunds = new List<HIS_BILL_FUND>();
		
        internal HisBillFundCreate()
            : base()
        {

        }

        internal HisBillFundCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_BILL_FUND data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBillFundCheck checker = new HisBillFundCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisBillFundDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBillFund_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBillFund that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisBillFunds.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisBillFunds))
            {
                if (!new HisBillFundTruncate(param).TruncateList(this.recentHisBillFunds))
                {
                    LogSystem.Warn("Rollback du lieu HisBillFund that bai, can kiem tra lai." + LogUtil.TraceData("recentHisBillFunds", this.recentHisBillFunds));
                }
            }
        }
    }
}
