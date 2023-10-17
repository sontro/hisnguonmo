using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestUser
{
    partial class HisExpMestUserCreate : BusinessBase
    {
		private List<HIS_EXP_MEST_USER> recentHisExpMestUsers = new List<HIS_EXP_MEST_USER>();
		
        internal HisExpMestUserCreate()
            : base()
        {

        }

        internal HisExpMestUserCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_EXP_MEST_USER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExpMestUserCheck checker = new HisExpMestUserCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.EXP_MEST_USER_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisExpMestUserDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMestUser_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisExpMestUser that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisExpMestUsers.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisExpMestUsers))
            {
                if (!new HisExpMestUserTruncate(param).TruncateList(this.recentHisExpMestUsers))
                {
                    LogSystem.Warn("Rollback du lieu HisExpMestUser that bai, can kiem tra lai." + LogUtil.TraceData("recentHisExpMestUsers", this.recentHisExpMestUsers));
                }
            }
        }
    }
}
