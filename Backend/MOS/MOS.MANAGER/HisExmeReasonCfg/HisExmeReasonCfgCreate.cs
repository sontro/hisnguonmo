using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExmeReasonCfg
{
    partial class HisExmeReasonCfgCreate : BusinessBase
    {
		private List<HIS_EXME_REASON_CFG> recentHisExmeReasonCfgs = new List<HIS_EXME_REASON_CFG>();
		
        internal HisExmeReasonCfgCreate()
            : base()
        {

        }

        internal HisExmeReasonCfgCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_EXME_REASON_CFG data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExmeReasonCfgCheck checker = new HisExmeReasonCfgCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsNotDuplicate(data, null);
                if (valid)
                {
					if (!DAOWorker.HisExmeReasonCfgDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExmeReasonCfg_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisExmeReasonCfg that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisExmeReasonCfgs.Add(data);
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
		
		internal bool CreateList(List<HIS_EXME_REASON_CFG> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExmeReasonCfgCheck checker = new HisExmeReasonCfgCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.IsNotDuplicate(data, null);
                }
                if (valid)
                {
                    if (!DAOWorker.HisExmeReasonCfgDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExmeReasonCfg_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisExmeReasonCfg that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisExmeReasonCfgs.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisExmeReasonCfgs))
            {
                if (!DAOWorker.HisExmeReasonCfgDAO.TruncateList(this.recentHisExmeReasonCfgs))
                {
                    LogSystem.Warn("Rollback du lieu HisExmeReasonCfg that bai, can kiem tra lai." + LogUtil.TraceData("recentHisExmeReasonCfgs", this.recentHisExmeReasonCfgs));
                }
				this.recentHisExmeReasonCfgs = null;
            }
        }
    }
}
