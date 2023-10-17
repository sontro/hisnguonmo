using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSaleProfitCfg
{
    partial class HisSaleProfitCfgUpdate : BusinessBase
    {
		private List<HIS_SALE_PROFIT_CFG> beforeUpdateHisSaleProfitCfgs = new List<HIS_SALE_PROFIT_CFG>();
		
        internal HisSaleProfitCfgUpdate()
            : base()
        {

        }

        internal HisSaleProfitCfgUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SALE_PROFIT_CFG data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSaleProfitCfgCheck checker = new HisSaleProfitCfgCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_SALE_PROFIT_CFG raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisSaleProfitCfgDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSaleProfitCfg_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSaleProfitCfg that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisSaleProfitCfgs.Add(raw);
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

        internal bool UpdateList(List<HIS_SALE_PROFIT_CFG> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSaleProfitCfgCheck checker = new HisSaleProfitCfgCheck(param);
                List<HIS_SALE_PROFIT_CFG> listRaw = new List<HIS_SALE_PROFIT_CFG>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisSaleProfitCfgDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSaleProfitCfg_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSaleProfitCfg that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisSaleProfitCfgs.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisSaleProfitCfgs))
            {
                if (!DAOWorker.HisSaleProfitCfgDAO.UpdateList(this.beforeUpdateHisSaleProfitCfgs))
                {
                    LogSystem.Warn("Rollback du lieu HisSaleProfitCfg that bai, can kiem tra lai." + LogUtil.TraceData("HisSaleProfitCfgs", this.beforeUpdateHisSaleProfitCfgs));
                }
				this.beforeUpdateHisSaleProfitCfgs = null;
            }
        }
    }
}
