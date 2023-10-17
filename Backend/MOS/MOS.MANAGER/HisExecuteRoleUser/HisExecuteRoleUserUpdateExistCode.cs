using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExecuteRoleUser
{
    partial class HisExecuteRoleUserUpdate : BusinessBase
    {
		private List<HIS_EXECUTE_ROLE_USER> beforeUpdateHisExecuteRoleUsers = new List<HIS_EXECUTE_ROLE_USER>();
		
        internal HisExecuteRoleUserUpdate()
            : base()
        {

        }

        internal HisExecuteRoleUserUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_EXECUTE_ROLE_USER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExecuteRoleUserCheck checker = new HisExecuteRoleUserCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_EXECUTE_ROLE_USER raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.EXECUTE_ROLE_USER_CODE, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisExecuteRoleUsers.Add(raw);
					if (!DAOWorker.HisExecuteRoleUserDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExecuteRoleUser_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisExecuteRoleUser that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_EXECUTE_ROLE_USER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExecuteRoleUserCheck checker = new HisExecuteRoleUserCheck(param);
                List<HIS_EXECUTE_ROLE_USER> listRaw = new List<HIS_EXECUTE_ROLE_USER>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.EXECUTE_ROLE_USER_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisExecuteRoleUsers.AddRange(listRaw);
					if (!DAOWorker.HisExecuteRoleUserDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExecuteRoleUser_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisExecuteRoleUser that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisExecuteRoleUsers))
            {
                if (!new HisExecuteRoleUserUpdate(param).UpdateList(this.beforeUpdateHisExecuteRoleUsers))
                {
                    LogSystem.Warn("Rollback du lieu HisExecuteRoleUser that bai, can kiem tra lai." + LogUtil.TraceData("HisExecuteRoleUsers", this.beforeUpdateHisExecuteRoleUsers));
                }
            }
        }
    }
}
