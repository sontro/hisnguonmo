using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBranchTime
{
    partial class HisBranchTimeUpdate : BusinessBase
    {
		private List<HIS_BRANCH_TIME> beforeUpdateHisBranchTimes = new List<HIS_BRANCH_TIME>();
		
        internal HisBranchTimeUpdate()
            : base()
        {

        }

        internal HisBranchTimeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_BRANCH_TIME data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBranchTimeCheck checker = new HisBranchTimeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_BRANCH_TIME raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisBranchTimeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBranchTime_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBranchTime that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisBranchTimes.Add(raw);
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

        internal bool UpdateList(List<HIS_BRANCH_TIME> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBranchTimeCheck checker = new HisBranchTimeCheck(param);
                List<HIS_BRANCH_TIME> listRaw = new List<HIS_BRANCH_TIME>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisBranchTimeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBranchTime_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBranchTime that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisBranchTimes.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisBranchTimes))
            {
                if (!DAOWorker.HisBranchTimeDAO.UpdateList(this.beforeUpdateHisBranchTimes))
                {
                    LogSystem.Warn("Rollback du lieu HisBranchTime that bai, can kiem tra lai." + LogUtil.TraceData("HisBranchTimes", this.beforeUpdateHisBranchTimes));
                }
				this.beforeUpdateHisBranchTimes = null;
            }
        }
    }
}
