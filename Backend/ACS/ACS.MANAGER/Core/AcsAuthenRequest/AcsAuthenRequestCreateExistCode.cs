using Inventec.Common.Logging;
using Inventec.Core;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.AcsAuthenRequest
{
    partial class AcsAuthenRequestCreate : BusinessBase
    {
		private List<ACS_AUTHEN_REQUEST> recentAcsAuthenRequests = new List<ACS_AUTHEN_REQUEST>();
		
        internal AcsAuthenRequestCreate()
            : base()
        {

        }

        internal AcsAuthenRequestCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(ACS_AUTHEN_REQUEST data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                AcsAuthenRequestCheck checker = new AcsAuthenRequestCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.AUTHEN_REQUEST_CODE, null);
                if (valid)
                {
					if (!DAOWorker.AcsAuthenRequestDAO.Create(data))
                    {
                        ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.AcsAuthenRequest_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin AcsAuthenRequest that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentAcsAuthenRequests.Add(data);
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
            if (IsNotNullOrEmpty(this.recentAcsAuthenRequests))
            {
                if (!DAOWorker.AcsAuthenRequestDAO.TruncateList(this.recentAcsAuthenRequests))
                {
                    LogSystem.Warn("Rollback du lieu AcsAuthenRequest that bai, can kiem tra lai." + LogUtil.TraceData("recentAcsAuthenRequests", this.recentAcsAuthenRequests));
                }
				this.recentAcsAuthenRequests = null;
            }
        }
    }
}
