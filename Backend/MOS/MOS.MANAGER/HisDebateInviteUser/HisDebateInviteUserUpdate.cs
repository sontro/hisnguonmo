using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisDebateInviteUser
{
    partial class HisDebateInviteUserUpdate : BusinessBase
    {
		private List<HIS_DEBATE_INVITE_USER> beforeUpdateHisDebateInviteUsers = new List<HIS_DEBATE_INVITE_USER>();
		
        internal HisDebateInviteUserUpdate()
            : base()
        {

        }

        internal HisDebateInviteUserUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_DEBATE_INVITE_USER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDebateInviteUserCheck checker = new HisDebateInviteUserCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_DEBATE_INVITE_USER raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisDebateInviteUserDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDebateInviteUser_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisDebateInviteUser that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisDebateInviteUsers.Add(raw);
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

        internal bool UpdateList(List<HIS_DEBATE_INVITE_USER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDebateInviteUserCheck checker = new HisDebateInviteUserCheck(param);
                List<HIS_DEBATE_INVITE_USER> listRaw = new List<HIS_DEBATE_INVITE_USER>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisDebateInviteUserDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDebateInviteUser_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisDebateInviteUser that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisDebateInviteUsers.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisDebateInviteUsers))
            {
                if (!DAOWorker.HisDebateInviteUserDAO.UpdateList(this.beforeUpdateHisDebateInviteUsers))
                {
                    LogSystem.Warn("Rollback du lieu HisDebateInviteUser that bai, can kiem tra lai." + LogUtil.TraceData("HisDebateInviteUsers", this.beforeUpdateHisDebateInviteUsers));
                }
				this.beforeUpdateHisDebateInviteUsers = null;
            }
        }
    }
}
