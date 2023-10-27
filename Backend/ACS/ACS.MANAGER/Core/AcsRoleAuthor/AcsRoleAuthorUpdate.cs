using Inventec.Common.Logging;
using Inventec.Core;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.MANAGER.AcsRoleAuthor
{
    partial class AcsRoleAuthorUpdate : BusinessBase
    {
		private List<ACS_ROLE_AUTHOR> beforeUpdateAcsRoleAuthors = new List<ACS_ROLE_AUTHOR>();
		
        internal AcsRoleAuthorUpdate()
            : base()
        {

        }

        internal AcsRoleAuthorUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(ACS_ROLE_AUTHOR data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                AcsRoleAuthorCheck checker = new AcsRoleAuthorCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                ACS_ROLE_AUTHOR raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.AcsRoleAuthorDAO.Update(data))
                    {
                        ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.AcsRoleAuthor_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin AcsRoleAuthor that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateAcsRoleAuthors.Add(raw);
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

        internal bool UpdateList(List<ACS_ROLE_AUTHOR> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                AcsRoleAuthorCheck checker = new AcsRoleAuthorCheck(param);
                List<ACS_ROLE_AUTHOR> listRaw = new List<ACS_ROLE_AUTHOR>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.AcsRoleAuthorDAO.UpdateList(listData))
                    {
                        ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.AcsRoleAuthor_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin AcsRoleAuthor that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateAcsRoleAuthors.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateAcsRoleAuthors))
            {
                if (!DAOWorker.AcsRoleAuthorDAO.UpdateList(this.beforeUpdateAcsRoleAuthors))
                {
                    LogSystem.Warn("Rollback du lieu AcsRoleAuthor that bai, can kiem tra lai." + LogUtil.TraceData("AcsRoleAuthors", this.beforeUpdateAcsRoleAuthors));
                }
				this.beforeUpdateAcsRoleAuthors = null;
            }
        }
    }
}
