using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBhytWhitelist
{
    partial class HisBhytWhitelistCreate : BusinessBase
    {
		private List<HIS_BHYT_WHITELIST> recentHisBhytWhitelists = new List<HIS_BHYT_WHITELIST>();
		
        internal HisBhytWhitelistCreate()
            : base()
        {

        }

        internal HisBhytWhitelistCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_BHYT_WHITELIST data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBhytWhitelistCheck checker = new HisBhytWhitelistCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisBhytWhitelistDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBhytWhitelist_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBhytWhitelist that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisBhytWhitelists.Add(data);
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
		
		internal bool CreateList(List<HIS_BHYT_WHITELIST> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBhytWhitelistCheck checker = new HisBhytWhitelistCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisBhytWhitelistDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBhytWhitelist_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBhytWhitelist that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisBhytWhitelists.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisBhytWhitelists))
            {
                if (!new HisBhytWhitelistTruncate(param).TruncateList(this.recentHisBhytWhitelists))
                {
                    LogSystem.Warn("Rollback du lieu HisBhytWhitelist that bai, can kiem tra lai." + LogUtil.TraceData("recentHisBhytWhitelists", this.recentHisBhytWhitelists));
                }
            }
        }
    }
}
