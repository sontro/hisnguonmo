using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisWarningFeeCfg
{
    partial class HisWarningFeeCfgCreate : BusinessBase
    {
		private List<HIS_WARNING_FEE_CFG> recentHisWarningFeeCfgs = new List<HIS_WARNING_FEE_CFG>();
		
        internal HisWarningFeeCfgCreate()
            : base()
        {

        }

        internal HisWarningFeeCfgCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_WARNING_FEE_CFG data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisWarningFeeCfgCheck checker = new HisWarningFeeCfgCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.WARNING_FEE_CFG_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisWarningFeeCfgDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisWarningFeeCfg_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisWarningFeeCfg that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisWarningFeeCfgs.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisWarningFeeCfgs))
            {
                if (!DAOWorker.HisWarningFeeCfgDAO.TruncateList(this.recentHisWarningFeeCfgs))
                {
                    LogSystem.Warn("Rollback du lieu HisWarningFeeCfg that bai, can kiem tra lai." + LogUtil.TraceData("recentHisWarningFeeCfgs", this.recentHisWarningFeeCfgs));
                }
				this.recentHisWarningFeeCfgs = null;
            }
        }
    }
}
