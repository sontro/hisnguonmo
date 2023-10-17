using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpiredDateCfg
{
    partial class HisExpiredDateCfgCreate : BusinessBase
    {
		private List<HIS_EXPIRED_DATE_CFG> recentHisExpiredDateCfgs = new List<HIS_EXPIRED_DATE_CFG>();
		
        internal HisExpiredDateCfgCreate()
            : base()
        {

        }

        internal HisExpiredDateCfgCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_EXPIRED_DATE_CFG data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExpiredDateCfgCheck checker = new HisExpiredDateCfgCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisExpiredDateCfgDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpiredDateCfg_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisExpiredDateCfg that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisExpiredDateCfgs.Add(data);
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
		
		internal bool CreateList(List<HIS_EXPIRED_DATE_CFG> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExpiredDateCfgCheck checker = new HisExpiredDateCfgCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisExpiredDateCfgDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpiredDateCfg_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisExpiredDateCfg that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisExpiredDateCfgs.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisExpiredDateCfgs))
            {
                if (!DAOWorker.HisExpiredDateCfgDAO.TruncateList(this.recentHisExpiredDateCfgs))
                {
                    LogSystem.Warn("Rollback du lieu HisExpiredDateCfg that bai, can kiem tra lai." + LogUtil.TraceData("recentHisExpiredDateCfgs", this.recentHisExpiredDateCfgs));
                }
				this.recentHisExpiredDateCfgs = null;
            }
        }
    }
}
