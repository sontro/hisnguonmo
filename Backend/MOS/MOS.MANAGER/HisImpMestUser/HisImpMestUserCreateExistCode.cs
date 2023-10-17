using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMestUser
{
    partial class HisImpMestUserCreate : BusinessBase
    {
		private List<HIS_IMP_MEST_USER> recentHisImpMestUsers = new List<HIS_IMP_MEST_USER>();
		
        internal HisImpMestUserCreate()
            : base()
        {

        }

        internal HisImpMestUserCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_IMP_MEST_USER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisImpMestUserCheck checker = new HisImpMestUserCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.IMP_MEST_USER_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisImpMestUserDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMestUser_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisImpMestUser that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisImpMestUsers.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisImpMestUsers))
            {
                if (!new HisImpMestUserTruncate(param).TruncateList(this.recentHisImpMestUsers))
                {
                    LogSystem.Warn("Rollback du lieu HisImpMestUser that bai, can kiem tra lai." + LogUtil.TraceData("recentHisImpMestUsers", this.recentHisImpMestUsers));
                }
            }
        }
    }
}
