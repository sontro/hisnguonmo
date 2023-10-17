using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBillFund
{
    partial class HisBillFundUpdate : BusinessBase
    {
		private List<HIS_BILL_FUND> beforeUpdateHisBillFunds = new List<HIS_BILL_FUND>();
		
        internal HisBillFundUpdate()
            : base()
        {

        }

        internal HisBillFundUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_BILL_FUND data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBillFundCheck checker = new HisBillFundCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_BILL_FUND raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.BILL_FUND_CODE, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisBillFunds.Add(raw);
					if (!DAOWorker.HisBillFundDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBillFund_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBillFund that bai." + LogUtil.TraceData("data", data));
                    }
                    
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

        internal bool UpdateList(List<HIS_BILL_FUND> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBillFundCheck checker = new HisBillFundCheck(param);
                List<HIS_BILL_FUND> listRaw = new List<HIS_BILL_FUND>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.BILL_FUND_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisBillFunds.AddRange(listRaw);
					if (!DAOWorker.HisBillFundDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBillFund_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBillFund that bai." + LogUtil.TraceData("listData", listData));
                    }
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisBillFunds))
            {
                if (!new HisBillFundUpdate(param).UpdateList(this.beforeUpdateHisBillFunds))
                {
                    LogSystem.Warn("Rollback du lieu HisBillFund that bai, can kiem tra lai." + LogUtil.TraceData("HisBillFunds", this.beforeUpdateHisBillFunds));
                }
            }
        }
    }
}
