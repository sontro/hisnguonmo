using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisNumOrderIssue
{
    partial class HisNumOrderIssueCreate : BusinessBase
    {
		private List<HIS_NUM_ORDER_ISSUE> recentHisNumOrderIssues = new List<HIS_NUM_ORDER_ISSUE>();
		
        internal HisNumOrderIssueCreate()
            : base()
        {

        }

        internal HisNumOrderIssueCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_NUM_ORDER_ISSUE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisNumOrderIssueCheck checker = new HisNumOrderIssueCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisNumOrderIssueDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisNumOrderIssue_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisNumOrderIssue that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisNumOrderIssues.Add(data);
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
		
		internal bool CreateList(List<HIS_NUM_ORDER_ISSUE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisNumOrderIssueCheck checker = new HisNumOrderIssueCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisNumOrderIssueDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisNumOrderIssue_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisNumOrderIssue that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisNumOrderIssues.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisNumOrderIssues))
            {
                if (!DAOWorker.HisNumOrderIssueDAO.TruncateList(this.recentHisNumOrderIssues))
                {
                    LogSystem.Warn("Rollback du lieu HisNumOrderIssue that bai, can kiem tra lai." + LogUtil.TraceData("recentHisNumOrderIssues", this.recentHisNumOrderIssues));
                }
				this.recentHisNumOrderIssues = null;
            }
        }
    }
}
