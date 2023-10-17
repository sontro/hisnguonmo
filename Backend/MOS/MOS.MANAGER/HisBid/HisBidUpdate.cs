using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBid
{
    partial class HisBidUpdate : BusinessBase
    {
		private List<HIS_BID> beforeUpdateHisBids = new List<HIS_BID>();
		
        internal HisBidUpdate()
            : base()
        {

        }

        internal HisBidUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_BID data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBidCheck checker = new HisBidCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_BID raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisBids.Add(raw);
					if (!DAOWorker.HisBidDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBid_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBid that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_BID> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBidCheck checker = new HisBidCheck(param);
                List<HIS_BID> listRaw = new List<HIS_BID>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisBids.AddRange(listRaw);
					if (!DAOWorker.HisBidDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBid_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBid that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisBids))
            {
                if (!new HisBidUpdate(param).UpdateList(this.beforeUpdateHisBids))
                {
                    LogSystem.Warn("Rollback du lieu HisBid that bai, can kiem tra lai." + LogUtil.TraceData("HisBids", this.beforeUpdateHisBids));
                }
            }
        }
    }
}
