using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBhytBlacklist
{
    partial class HisBhytBlacklistCreate : BusinessBase
    {
		private List<HIS_BHYT_BLACKLIST> recentHisBhytBlacklists = new List<HIS_BHYT_BLACKLIST>();
		
        internal HisBhytBlacklistCreate()
            : base()
        {

        }

        internal HisBhytBlacklistCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_BHYT_BLACKLIST data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBhytBlacklistCheck checker = new HisBhytBlacklistCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisBhytBlacklistDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBhytBlacklist_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBhytBlacklist that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisBhytBlacklists.Add(data);
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
		
		internal bool CreateList(List<HIS_BHYT_BLACKLIST> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBhytBlacklistCheck checker = new HisBhytBlacklistCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisBhytBlacklistDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBhytBlacklist_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBhytBlacklist that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisBhytBlacklists.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisBhytBlacklists))
            {
                if (!new HisBhytBlacklistTruncate(param).TruncateList(this.recentHisBhytBlacklists))
                {
                    LogSystem.Warn("Rollback du lieu HisBhytBlacklist that bai, can kiem tra lai." + LogUtil.TraceData("recentHisBhytBlacklists", this.recentHisBhytBlacklists));
                }
            }
        }
    }
}
