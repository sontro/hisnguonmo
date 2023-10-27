using Inventec.Common.Logging;
using Inventec.Core;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.AcsToken
{
    partial class AcsTokenCreate : BusinessBase
    {
		private List<ACS_TOKEN> recentAcsTokens = new List<ACS_TOKEN>();
		
        internal AcsTokenCreate()
            : base()
        {

        }

        internal AcsTokenCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(ACS_TOKEN data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                AcsTokenCheck checker = new AcsTokenCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.TOKEN_CODE, null);
                if (valid)
                {
					if (!DAOWorker.AcsTokenDAO.Create(data))
                    {
                        ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.AcsToken_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin AcsToken that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentAcsTokens.Add(data);
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
            if (IsNotNullOrEmpty(this.recentAcsTokens))
            {
                if (!DAOWorker.AcsTokenDAO.TruncateList(this.recentAcsTokens))
                {
                    LogSystem.Warn("Rollback du lieu AcsToken that bai, can kiem tra lai." + LogUtil.TraceData("recentAcsTokens", this.recentAcsTokens));
                }
				this.recentAcsTokens = null;
            }
        }
    }
}
