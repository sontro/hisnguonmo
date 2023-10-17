using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBhytWhitelist
{
    partial class HisBhytWhitelistUpdate : BusinessBase
    {
		private List<HIS_BHYT_WHITELIST> beforeUpdateHisBhytWhitelists = new List<HIS_BHYT_WHITELIST>();
		
        internal HisBhytWhitelistUpdate()
            : base()
        {

        }

        internal HisBhytWhitelistUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_BHYT_WHITELIST data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBhytWhitelistCheck checker = new HisBhytWhitelistCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_BHYT_WHITELIST raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisBhytWhitelists.Add(raw);
					if (!DAOWorker.HisBhytWhitelistDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBhytWhitelist_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBhytWhitelist that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_BHYT_WHITELIST> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBhytWhitelistCheck checker = new HisBhytWhitelistCheck(param);
                List<HIS_BHYT_WHITELIST> listRaw = new List<HIS_BHYT_WHITELIST>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisBhytWhitelists.AddRange(listRaw);
					if (!DAOWorker.HisBhytWhitelistDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBhytWhitelist_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBhytWhitelist that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisBhytWhitelists))
            {
                if (!new HisBhytWhitelistUpdate(param).UpdateList(this.beforeUpdateHisBhytWhitelists))
                {
                    LogSystem.Warn("Rollback du lieu HisBhytWhitelist that bai, can kiem tra lai." + LogUtil.TraceData("HisBhytWhitelists", this.beforeUpdateHisBhytWhitelists));
                }
            }
        }
    }
}
