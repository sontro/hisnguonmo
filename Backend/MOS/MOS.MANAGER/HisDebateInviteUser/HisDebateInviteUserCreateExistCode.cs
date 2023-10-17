using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDebateInviteUser
{
    partial class HisDebateInviteUserCreate : BusinessBase
    {
		private List<HIS_DEBATE_INVITE_USER> recentHisDebateInviteUsers = new List<HIS_DEBATE_INVITE_USER>();
		
        internal HisDebateInviteUserCreate()
            : base()
        {

        }

        internal HisDebateInviteUserCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_DEBATE_INVITE_USER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDebateInviteUserCheck checker = new HisDebateInviteUserCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.DEBATE_INVITE_USER_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisDebateInviteUserDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDebateInviteUser_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisDebateInviteUser that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisDebateInviteUsers.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisDebateInviteUsers))
            {
                if (!DAOWorker.HisDebateInviteUserDAO.TruncateList(this.recentHisDebateInviteUsers))
                {
                    LogSystem.Warn("Rollback du lieu HisDebateInviteUser that bai, can kiem tra lai." + LogUtil.TraceData("recentHisDebateInviteUsers", this.recentHisDebateInviteUsers));
                }
				this.recentHisDebateInviteUsers = null;
            }
        }
    }
}
