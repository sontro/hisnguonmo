using Inventec.Common.Logging;
using Inventec.Core;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.AcsRoleAuthor
{
    partial class AcsRoleAuthorCreate : BusinessBase
    {
		private List<ACS_ROLE_AUTHOR> recentAcsRoleAuthors = new List<ACS_ROLE_AUTHOR>();
		
        internal AcsRoleAuthorCreate()
            : base()
        {

        }

        internal AcsRoleAuthorCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(ACS_ROLE_AUTHOR data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                AcsRoleAuthorCheck checker = new AcsRoleAuthorCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.ROLE_AUTHOR_CODE, null);
                if (valid)
                {
					if (!DAOWorker.AcsRoleAuthorDAO.Create(data))
                    {
                        ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.AcsRoleAuthor_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin AcsRoleAuthor that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentAcsRoleAuthors.Add(data);
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
            if (IsNotNullOrEmpty(this.recentAcsRoleAuthors))
            {
                if (!DAOWorker.AcsRoleAuthorDAO.TruncateList(this.recentAcsRoleAuthors))
                {
                    LogSystem.Warn("Rollback du lieu AcsRoleAuthor that bai, can kiem tra lai." + LogUtil.TraceData("recentAcsRoleAuthors", this.recentAcsRoleAuthors));
                }
				this.recentAcsRoleAuthors = null;
            }
        }
    }
}
