using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMestTypeUser
{
    partial class HisImpMestTypeUserCreate : BusinessBase
    {
		private List<HIS_IMP_MEST_TYPE_USER> recentHisImpMestTypeUsers = new List<HIS_IMP_MEST_TYPE_USER>();
		
        internal HisImpMestTypeUserCreate()
            : base()
        {

        }

        internal HisImpMestTypeUserCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_IMP_MEST_TYPE_USER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisImpMestTypeUserCheck checker = new HisImpMestTypeUserCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.IMP_MEST_TYPE_USER_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisImpMestTypeUserDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMestTypeUser_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisImpMestTypeUser that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisImpMestTypeUsers.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisImpMestTypeUsers))
            {
                if (!new HisImpMestTypeUserTruncate(param).TruncateList(this.recentHisImpMestTypeUsers))
                {
                    LogSystem.Warn("Rollback du lieu HisImpMestTypeUser that bai, can kiem tra lai." + LogUtil.TraceData("recentHisImpMestTypeUsers", this.recentHisImpMestTypeUsers));
                }
            }
        }
    }
}
