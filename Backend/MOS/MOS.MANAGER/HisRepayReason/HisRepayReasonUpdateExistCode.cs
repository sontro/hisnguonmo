using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisRepayReason
{
    partial class HisRepayReasonUpdate : BusinessBase
    {
		private List<HIS_REPAY_REASON> beforeUpdateHisRepayReasons = new List<HIS_REPAY_REASON>();
		
        internal HisRepayReasonUpdate()
            : base()
        {

        }

        internal HisRepayReasonUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_REPAY_REASON data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRepayReasonCheck checker = new HisRepayReasonCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_REPAY_REASON raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.REPAY_REASON_CODE, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisRepayReasons.Add(raw);
					if (!DAOWorker.HisRepayReasonDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRepayReason_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisRepayReason that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_REPAY_REASON> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRepayReasonCheck checker = new HisRepayReasonCheck(param);
                List<HIS_REPAY_REASON> listRaw = new List<HIS_REPAY_REASON>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.REPAY_REASON_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisRepayReasons.AddRange(listRaw);
					if (!DAOWorker.HisRepayReasonDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRepayReason_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisRepayReason that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisRepayReasons))
            {
                if (!DAOWorker.HisRepayReasonDAO.UpdateList(this.beforeUpdateHisRepayReasons))
                {
                    LogSystem.Warn("Rollback du lieu HisRepayReason that bai, can kiem tra lai." + LogUtil.TraceData("HisRepayReasons", this.beforeUpdateHisRepayReasons));
                }
            }
        }
    }
}
