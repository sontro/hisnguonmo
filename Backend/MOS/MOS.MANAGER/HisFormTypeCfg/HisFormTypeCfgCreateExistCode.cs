using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisFormTypeCfg
{
    partial class HisFormTypeCfgCreate : BusinessBase
    {
		private List<HIS_FORM_TYPE_CFG> recentHisFormTypeCfgs = new List<HIS_FORM_TYPE_CFG>();
		
        internal HisFormTypeCfgCreate()
            : base()
        {

        }

        internal HisFormTypeCfgCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_FORM_TYPE_CFG data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisFormTypeCfgCheck checker = new HisFormTypeCfgCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.FORM_TYPE_CFG_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisFormTypeCfgDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisFormTypeCfg_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisFormTypeCfg that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisFormTypeCfgs.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisFormTypeCfgs))
            {
                if (!DAOWorker.HisFormTypeCfgDAO.TruncateList(this.recentHisFormTypeCfgs))
                {
                    LogSystem.Warn("Rollback du lieu HisFormTypeCfg that bai, can kiem tra lai." + LogUtil.TraceData("recentHisFormTypeCfgs", this.recentHisFormTypeCfgs));
                }
				this.recentHisFormTypeCfgs = null;
            }
        }
    }
}
