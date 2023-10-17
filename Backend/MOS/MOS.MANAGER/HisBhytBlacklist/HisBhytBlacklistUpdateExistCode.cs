using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBhytBlacklist
{
    partial class HisBhytBlacklistUpdate : BusinessBase
    {
		private List<HIS_BHYT_BLACKLIST> beforeUpdateHisBhytBlacklists = new List<HIS_BHYT_BLACKLIST>();
		
        internal HisBhytBlacklistUpdate()
            : base()
        {

        }

        internal HisBhytBlacklistUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_BHYT_BLACKLIST data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBhytBlacklistCheck checker = new HisBhytBlacklistCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_BHYT_BLACKLIST raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.HEIN_CARD_NUMBER, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisBhytBlacklists.Add(raw);
					if (!DAOWorker.HisBhytBlacklistDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBhytBlacklist_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBhytBlacklist that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_BHYT_BLACKLIST> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBhytBlacklistCheck checker = new HisBhytBlacklistCheck(param);
                List<HIS_BHYT_BLACKLIST> listRaw = new List<HIS_BHYT_BLACKLIST>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.HEIN_CARD_NUMBER, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisBhytBlacklists.AddRange(listRaw);
					if (!DAOWorker.HisBhytBlacklistDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBhytBlacklist_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBhytBlacklist that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisBhytBlacklists))
            {
                if (!new HisBhytBlacklistUpdate(param).UpdateList(this.beforeUpdateHisBhytBlacklists))
                {
                    LogSystem.Warn("Rollback du lieu HisBhytBlacklist that bai, can kiem tra lai." + LogUtil.TraceData("HisBhytBlacklists", this.beforeUpdateHisBhytBlacklists));
                }
            }
        }
    }
}
