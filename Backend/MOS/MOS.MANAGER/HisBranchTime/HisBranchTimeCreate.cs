using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBranchTime
{
    partial class HisBranchTimeCreate : BusinessBase
    {
		private List<HIS_BRANCH_TIME> recentHisBranchTimes = new List<HIS_BRANCH_TIME>();
		
        internal HisBranchTimeCreate()
            : base()
        {

        }

        internal HisBranchTimeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_BRANCH_TIME data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBranchTimeCheck checker = new HisBranchTimeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisBranchTimeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBranchTime_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBranchTime that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisBranchTimes.Add(data);
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
		
		internal bool CreateList(List<HIS_BRANCH_TIME> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBranchTimeCheck checker = new HisBranchTimeCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisBranchTimeDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBranchTime_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBranchTime that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisBranchTimes.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisBranchTimes))
            {
                if (!DAOWorker.HisBranchTimeDAO.TruncateList(this.recentHisBranchTimes))
                {
                    LogSystem.Warn("Rollback du lieu HisBranchTime that bai, can kiem tra lai." + LogUtil.TraceData("recentHisBranchTimes", this.recentHisBranchTimes));
                }
				this.recentHisBranchTimes = null;
            }
        }
    }
}
