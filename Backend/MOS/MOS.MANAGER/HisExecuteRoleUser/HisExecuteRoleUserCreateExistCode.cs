using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExecuteRoleUser
{
    partial class HisExecuteRoleUserCreate : BusinessBase
    {
		private List<HIS_EXECUTE_ROLE_USER> recentHisExecuteRoleUsers = new List<HIS_EXECUTE_ROLE_USER>();
		
        internal HisExecuteRoleUserCreate()
            : base()
        {

        }

        internal HisExecuteRoleUserCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_EXECUTE_ROLE_USER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExecuteRoleUserCheck checker = new HisExecuteRoleUserCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.EXECUTE_ROLE_USER_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisExecuteRoleUserDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExecuteRoleUser_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisExecuteRoleUser that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisExecuteRoleUsers.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisExecuteRoleUsers))
            {
                if (!new HisExecuteRoleUserTruncate(param).TruncateList(this.recentHisExecuteRoleUsers))
                {
                    LogSystem.Warn("Rollback du lieu HisExecuteRoleUser that bai, can kiem tra lai." + LogUtil.TraceData("recentHisExecuteRoleUsers", this.recentHisExecuteRoleUsers));
                }
            }
        }
    }
}
