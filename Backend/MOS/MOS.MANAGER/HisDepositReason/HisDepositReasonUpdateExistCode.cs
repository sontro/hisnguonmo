using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisDepositReason
{
    partial class HisDepositReasonUpdate : BusinessBase
    {
		private List<HIS_DEPOSIT_REASON> beforeUpdateHisDepositReasons = new List<HIS_DEPOSIT_REASON>();
		
        internal HisDepositReasonUpdate()
            : base()
        {

        }

        internal HisDepositReasonUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_DEPOSIT_REASON data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDepositReasonCheck checker = new HisDepositReasonCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_DEPOSIT_REASON raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.DEPOSIT_REASON_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisDepositReasonDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDepositReason_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisDepositReason that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisDepositReasons.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_DEPOSIT_REASON> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDepositReasonCheck checker = new HisDepositReasonCheck(param);
                List<HIS_DEPOSIT_REASON> listRaw = new List<HIS_DEPOSIT_REASON>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.DEPOSIT_REASON_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisDepositReasonDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDepositReason_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisDepositReason that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisDepositReasons.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisDepositReasons))
            {
                if (!DAOWorker.HisDepositReasonDAO.UpdateList(this.beforeUpdateHisDepositReasons))
                {
                    LogSystem.Warn("Rollback du lieu HisDepositReason that bai, can kiem tra lai." + LogUtil.TraceData("HisDepositReasons", this.beforeUpdateHisDepositReasons));
                }
				this.beforeUpdateHisDepositReasons = null;
            }
        }
    }
}
