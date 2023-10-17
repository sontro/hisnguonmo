using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSaleProfitCfg
{
    partial class HisSaleProfitCfgCreate : BusinessBase
    {
		private List<HIS_SALE_PROFIT_CFG> recentHisSaleProfitCfgs = new List<HIS_SALE_PROFIT_CFG>();
		
        internal HisSaleProfitCfgCreate()
            : base()
        {

        }

        internal HisSaleProfitCfgCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SALE_PROFIT_CFG data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSaleProfitCfgCheck checker = new HisSaleProfitCfgCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisSaleProfitCfgDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSaleProfitCfg_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSaleProfitCfg that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisSaleProfitCfgs.Add(data);
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
		
		internal bool CreateList(List<HIS_SALE_PROFIT_CFG> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSaleProfitCfgCheck checker = new HisSaleProfitCfgCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisSaleProfitCfgDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSaleProfitCfg_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSaleProfitCfg that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisSaleProfitCfgs.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisSaleProfitCfgs))
            {
                if (!DAOWorker.HisSaleProfitCfgDAO.TruncateList(this.recentHisSaleProfitCfgs))
                {
                    LogSystem.Warn("Rollback du lieu HisSaleProfitCfg that bai, can kiem tra lai." + LogUtil.TraceData("recentHisSaleProfitCfgs", this.recentHisSaleProfitCfgs));
                }
				this.recentHisSaleProfitCfgs = null;
            }
        }
    }
}
