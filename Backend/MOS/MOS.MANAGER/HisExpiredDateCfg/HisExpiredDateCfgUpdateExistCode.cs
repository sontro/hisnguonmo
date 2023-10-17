using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExpiredDateCfg
{
    partial class HisExpiredDateCfgUpdate : BusinessBase
    {
		private List<HIS_EXPIRED_DATE_CFG> beforeUpdateHisExpiredDateCfgs = new List<HIS_EXPIRED_DATE_CFG>();
		
        internal HisExpiredDateCfgUpdate()
            : base()
        {

        }

        internal HisExpiredDateCfgUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_EXPIRED_DATE_CFG data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExpiredDateCfgCheck checker = new HisExpiredDateCfgCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_EXPIRED_DATE_CFG raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.EXPIRED_DATE_CFG_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisExpiredDateCfgDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpiredDateCfg_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisExpiredDateCfg that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisExpiredDateCfgs.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_EXPIRED_DATE_CFG> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExpiredDateCfgCheck checker = new HisExpiredDateCfgCheck(param);
                List<HIS_EXPIRED_DATE_CFG> listRaw = new List<HIS_EXPIRED_DATE_CFG>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.EXPIRED_DATE_CFG_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisExpiredDateCfgDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpiredDateCfg_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisExpiredDateCfg that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisExpiredDateCfgs.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisExpiredDateCfgs))
            {
                if (!DAOWorker.HisExpiredDateCfgDAO.UpdateList(this.beforeUpdateHisExpiredDateCfgs))
                {
                    LogSystem.Warn("Rollback du lieu HisExpiredDateCfg that bai, can kiem tra lai." + LogUtil.TraceData("HisExpiredDateCfgs", this.beforeUpdateHisExpiredDateCfgs));
                }
				this.beforeUpdateHisExpiredDateCfgs = null;
            }
        }
    }
}
