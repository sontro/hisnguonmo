using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisUnlimitReason
{
    partial class HisUnlimitReasonUpdate : BusinessBase
    {
		private List<HIS_UNLIMIT_REASON> beforeUpdateHisUnlimitReasons = new List<HIS_UNLIMIT_REASON>();
		
        internal HisUnlimitReasonUpdate()
            : base()
        {

        }

        internal HisUnlimitReasonUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_UNLIMIT_REASON data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisUnlimitReasonCheck checker = new HisUnlimitReasonCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_UNLIMIT_REASON raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisUnlimitReasonDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisUnlimitReason_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisUnlimitReason that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisUnlimitReasons.Add(raw);
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

        internal bool UpdateList(List<HIS_UNLIMIT_REASON> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisUnlimitReasonCheck checker = new HisUnlimitReasonCheck(param);
                List<HIS_UNLIMIT_REASON> listRaw = new List<HIS_UNLIMIT_REASON>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisUnlimitReasonDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisUnlimitReason_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisUnlimitReason that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisUnlimitReasons.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisUnlimitReasons))
            {
                if (!DAOWorker.HisUnlimitReasonDAO.UpdateList(this.beforeUpdateHisUnlimitReasons))
                {
                    LogSystem.Warn("Rollback du lieu HisUnlimitReason that bai, can kiem tra lai." + LogUtil.TraceData("HisUnlimitReasons", this.beforeUpdateHisUnlimitReasons));
                }
				this.beforeUpdateHisUnlimitReasons = null;
            }
        }
    }
}
