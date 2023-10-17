using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPtttGroupBest
{
    partial class HisPtttGroupBestUpdate : BusinessBase
    {
		private List<HIS_PTTT_GROUP_BEST> beforeUpdateHisPtttGroupBests = new List<HIS_PTTT_GROUP_BEST>();
		
        internal HisPtttGroupBestUpdate()
            : base()
        {

        }

        internal HisPtttGroupBestUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_PTTT_GROUP_BEST data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPtttGroupBestCheck checker = new HisPtttGroupBestCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_PTTT_GROUP_BEST raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisPtttGroupBestDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPtttGroupBest_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPtttGroupBest that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisPtttGroupBests.Add(raw);
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

        internal bool UpdateList(List<HIS_PTTT_GROUP_BEST> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPtttGroupBestCheck checker = new HisPtttGroupBestCheck(param);
                List<HIS_PTTT_GROUP_BEST> listRaw = new List<HIS_PTTT_GROUP_BEST>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisPtttGroupBestDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPtttGroupBest_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPtttGroupBest that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisPtttGroupBests.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisPtttGroupBests))
            {
                if (!DAOWorker.HisPtttGroupBestDAO.UpdateList(this.beforeUpdateHisPtttGroupBests))
                {
                    LogSystem.Warn("Rollback du lieu HisPtttGroupBest that bai, can kiem tra lai." + LogUtil.TraceData("HisPtttGroupBests", this.beforeUpdateHisPtttGroupBests));
                }
				this.beforeUpdateHisPtttGroupBests = null;
            }
        }
    }
}
