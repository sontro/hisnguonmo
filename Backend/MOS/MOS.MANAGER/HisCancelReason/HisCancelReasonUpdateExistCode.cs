using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisCancelReason
{
    partial class HisCancelReasonUpdate : BusinessBase
    {
		private List<HIS_CANCEL_REASON> beforeUpdateHisCancelReasons = new List<HIS_CANCEL_REASON>();
		
        internal HisCancelReasonUpdate()
            : base()
        {

        }

        internal HisCancelReasonUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_CANCEL_REASON data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCancelReasonCheck checker = new HisCancelReasonCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_CANCEL_REASON raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.CANCEL_REASON_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisCancelReasonDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCancelReason_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisCancelReason that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisCancelReasons.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_CANCEL_REASON> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisCancelReasonCheck checker = new HisCancelReasonCheck(param);
                List<HIS_CANCEL_REASON> listRaw = new List<HIS_CANCEL_REASON>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.CANCEL_REASON_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisCancelReasonDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCancelReason_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisCancelReason that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisCancelReasons.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisCancelReasons))
            {
                if (!DAOWorker.HisCancelReasonDAO.UpdateList(this.beforeUpdateHisCancelReasons))
                {
                    LogSystem.Warn("Rollback du lieu HisCancelReason that bai, can kiem tra lai." + LogUtil.TraceData("HisCancelReasons", this.beforeUpdateHisCancelReasons));
                }
				this.beforeUpdateHisCancelReasons = null;
            }
        }
    }
}
