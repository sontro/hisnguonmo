using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEkipPlanUser
{
    partial class HisEkipPlanUserCreate : BusinessBase
    {
		private List<HIS_EKIP_PLAN_USER> recentHisEkipPlanUsers = new List<HIS_EKIP_PLAN_USER>();
		
        internal HisEkipPlanUserCreate()
            : base()
        {

        }

        internal HisEkipPlanUserCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_EKIP_PLAN_USER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEkipPlanUserCheck checker = new HisEkipPlanUserCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.EKIP_PLAN_USER_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisEkipPlanUserDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEkipPlanUser_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisEkipPlanUser that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisEkipPlanUsers.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisEkipPlanUsers))
            {
                if (!DAOWorker.HisEkipPlanUserDAO.TruncateList(this.recentHisEkipPlanUsers))
                {
                    LogSystem.Warn("Rollback du lieu HisEkipPlanUser that bai, can kiem tra lai." + LogUtil.TraceData("recentHisEkipPlanUsers", this.recentHisEkipPlanUsers));
                }
				this.recentHisEkipPlanUsers = null;
            }
        }
    }
}
