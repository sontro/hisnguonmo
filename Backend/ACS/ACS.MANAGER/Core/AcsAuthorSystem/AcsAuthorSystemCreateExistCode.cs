using Inventec.Common.Logging;
using Inventec.Core;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.AcsAuthorSystem
{
    partial class AcsAuthorSystemCreate : BusinessBase
    {
		private List<ACS_AUTHOR_SYSTEM> recentAcsAuthorSystems = new List<ACS_AUTHOR_SYSTEM>();
		
        internal AcsAuthorSystemCreate()
            : base()
        {

        }

        internal AcsAuthorSystemCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(ACS_AUTHOR_SYSTEM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                AcsAuthorSystemCheck checker = new AcsAuthorSystemCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.AUTHOR_SYSTEM_CODE, null);
                if (valid)
                {
					if (!DAOWorker.AcsAuthorSystemDAO.Create(data))
                    {
                        ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.AcsAuthorSystem_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin AcsAuthorSystem that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentAcsAuthorSystems.Add(data);
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
            if (IsNotNullOrEmpty(this.recentAcsAuthorSystems))
            {
                if (!DAOWorker.AcsAuthorSystemDAO.TruncateList(this.recentAcsAuthorSystems))
                {
                    LogSystem.Warn("Rollback du lieu AcsAuthorSystem that bai, can kiem tra lai." + LogUtil.TraceData("recentAcsAuthorSystems", this.recentAcsAuthorSystems));
                }
				this.recentAcsAuthorSystems = null;
            }
        }
    }
}
