using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisNumOrderIssue
{
    partial class HisNumOrderIssueUpdate : BusinessBase
    {
		private List<HIS_NUM_ORDER_ISSUE> beforeUpdateHisNumOrderIssues = new List<HIS_NUM_ORDER_ISSUE>();
		
        internal HisNumOrderIssueUpdate()
            : base()
        {

        }

        internal HisNumOrderIssueUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_NUM_ORDER_ISSUE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisNumOrderIssueCheck checker = new HisNumOrderIssueCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_NUM_ORDER_ISSUE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisNumOrderIssueDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisNumOrderIssue_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisNumOrderIssue that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisNumOrderIssues.Add(raw);
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

        internal bool UpdateList(List<HIS_NUM_ORDER_ISSUE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisNumOrderIssueCheck checker = new HisNumOrderIssueCheck(param);
                List<HIS_NUM_ORDER_ISSUE> listRaw = new List<HIS_NUM_ORDER_ISSUE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisNumOrderIssueDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisNumOrderIssue_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisNumOrderIssue that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisNumOrderIssues.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisNumOrderIssues))
            {
                if (!DAOWorker.HisNumOrderIssueDAO.UpdateList(this.beforeUpdateHisNumOrderIssues))
                {
                    LogSystem.Warn("Rollback du lieu HisNumOrderIssue that bai, can kiem tra lai." + LogUtil.TraceData("HisNumOrderIssues", this.beforeUpdateHisNumOrderIssues));
                }
				this.beforeUpdateHisNumOrderIssues = null;
            }
        }
    }
}
