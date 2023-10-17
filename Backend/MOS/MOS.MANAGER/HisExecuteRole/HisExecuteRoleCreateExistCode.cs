using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExecuteRole
{
    partial class HisExecuteRoleCreate : BusinessBase
    {
		private List<HIS_EXECUTE_ROLE> recentHisExecuteRoles = new List<HIS_EXECUTE_ROLE>();
		
        internal HisExecuteRoleCreate()
            : base()
        {

        }

        internal HisExecuteRoleCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_EXECUTE_ROLE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExecuteRoleCheck checker = new HisExecuteRoleCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.EXECUTE_ROLE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisExecuteRoleDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExecuteRole_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisExecuteRole that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisExecuteRoles.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisExecuteRoles))
            {
                if (!new HisExecuteRoleTruncate(param).TruncateList(this.recentHisExecuteRoles))
                {
                    LogSystem.Warn("Rollback du lieu HisExecuteRole that bai, can kiem tra lai." + LogUtil.TraceData("recentHisExecuteRoles", this.recentHisExecuteRoles));
                }
            }
        }
    }
}
