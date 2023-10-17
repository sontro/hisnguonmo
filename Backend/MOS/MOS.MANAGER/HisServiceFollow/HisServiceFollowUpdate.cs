using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceFollow
{
    partial class HisServiceFollowUpdate : BusinessBase
    {
		private List<HIS_SERVICE_FOLLOW> beforeUpdateHisServiceFollows = new List<HIS_SERVICE_FOLLOW>();
		
        internal HisServiceFollowUpdate()
            : base()
        {

        }

        internal HisServiceFollowUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SERVICE_FOLLOW data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceFollowCheck checker = new HisServiceFollowCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_SERVICE_FOLLOW raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisServiceFollows.Add(raw);
					if (!DAOWorker.HisServiceFollowDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceFollow_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisServiceFollow that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_SERVICE_FOLLOW> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceFollowCheck checker = new HisServiceFollowCheck(param);
                List<HIS_SERVICE_FOLLOW> listRaw = new List<HIS_SERVICE_FOLLOW>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisServiceFollows.AddRange(listRaw);
					if (!DAOWorker.HisServiceFollowDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceFollow_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisServiceFollow that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisServiceFollows))
            {
                if (!new HisServiceFollowUpdate(param).UpdateList(this.beforeUpdateHisServiceFollows))
                {
                    LogSystem.Warn("Rollback du lieu HisServiceFollow that bai, can kiem tra lai." + LogUtil.TraceData("HisServiceFollows", this.beforeUpdateHisServiceFollows));
                }
            }
        }
    }
}
