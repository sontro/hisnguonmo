using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMestInveUser
{
    partial class HisMestInveUserUpdate : BusinessBase
    {
		private List<HIS_MEST_INVE_USER> beforeUpdateHisMestInveUsers = new List<HIS_MEST_INVE_USER>();
		
        internal HisMestInveUserUpdate()
            : base()
        {

        }

        internal HisMestInveUserUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MEST_INVE_USER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMestInveUserCheck checker = new HisMestInveUserCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_MEST_INVE_USER raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisMestInveUsers.Add(raw);
					if (!DAOWorker.HisMestInveUserDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMestInveUser_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMestInveUser that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_MEST_INVE_USER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMestInveUserCheck checker = new HisMestInveUserCheck(param);
                List<HIS_MEST_INVE_USER> listRaw = new List<HIS_MEST_INVE_USER>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisMestInveUsers.AddRange(listRaw);
					if (!DAOWorker.HisMestInveUserDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMestInveUser_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMestInveUser that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMestInveUsers))
            {
                if (!new HisMestInveUserUpdate(param).UpdateList(this.beforeUpdateHisMestInveUsers))
                {
                    LogSystem.Warn("Rollback du lieu HisMestInveUser that bai, can kiem tra lai." + LogUtil.TraceData("HisMestInveUsers", this.beforeUpdateHisMestInveUsers));
                }
            }
        }
    }
}
