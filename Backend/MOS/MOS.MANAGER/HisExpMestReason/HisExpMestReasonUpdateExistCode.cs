using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExpMestReason
{
    partial class HisExpMestReasonUpdate : BusinessBase
    {
		private List<HIS_EXP_MEST_REASON> beforeUpdateHisExpMestReasons = new List<HIS_EXP_MEST_REASON>();
		
        internal HisExpMestReasonUpdate()
            : base()
        {

        }

        internal HisExpMestReasonUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_EXP_MEST_REASON data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExpMestReasonCheck checker = new HisExpMestReasonCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_EXP_MEST_REASON raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.EXP_MEST_REASON_CODE, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisExpMestReasons.Add(raw);
					if (!DAOWorker.HisExpMestReasonDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMestReason_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisExpMestReason that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_EXP_MEST_REASON> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExpMestReasonCheck checker = new HisExpMestReasonCheck(param);
                List<HIS_EXP_MEST_REASON> listRaw = new List<HIS_EXP_MEST_REASON>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.EXP_MEST_REASON_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisExpMestReasons.AddRange(listRaw);
					if (!DAOWorker.HisExpMestReasonDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMestReason_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisExpMestReason that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisExpMestReasons))
            {
                if (!new HisExpMestReasonUpdate(param).UpdateList(this.beforeUpdateHisExpMestReasons))
                {
                    LogSystem.Warn("Rollback du lieu HisExpMestReason that bai, can kiem tra lai." + LogUtil.TraceData("HisExpMestReasons", this.beforeUpdateHisExpMestReasons));
                }
            }
        }
    }
}
