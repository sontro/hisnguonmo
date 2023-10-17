using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExmeReasonCfg
{
    partial class HisExmeReasonCfgUpdate : BusinessBase
    {
		private List<HIS_EXME_REASON_CFG> beforeUpdateHisExmeReasonCfgs = new List<HIS_EXME_REASON_CFG>();
		
        internal HisExmeReasonCfgUpdate()
            : base()
        {

        }

        internal HisExmeReasonCfgUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_EXME_REASON_CFG data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExmeReasonCfgCheck checker = new HisExmeReasonCfgCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_EXME_REASON_CFG raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsNotDuplicate(data, raw);
                if (valid)
                {                    
					if (!DAOWorker.HisExmeReasonCfgDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExmeReasonCfg_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisExmeReasonCfg that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisExmeReasonCfgs.Add(raw);
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

        internal bool UpdateList(List<HIS_EXME_REASON_CFG> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExmeReasonCfgCheck checker = new HisExmeReasonCfgCheck(param);
                List<HIS_EXME_REASON_CFG> listRaw = new List<HIS_EXME_REASON_CFG>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.IsNotDuplicate(data, listRaw.FirstOrDefault(o => o.ID == data.ID));
                }
                if (valid)
                {
					if (!DAOWorker.HisExmeReasonCfgDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExmeReasonCfg_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisExmeReasonCfg that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisExmeReasonCfgs.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisExmeReasonCfgs))
            {
                if (!DAOWorker.HisExmeReasonCfgDAO.UpdateList(this.beforeUpdateHisExmeReasonCfgs))
                {
                    LogSystem.Warn("Rollback du lieu HisExmeReasonCfg that bai, can kiem tra lai." + LogUtil.TraceData("HisExmeReasonCfgs", this.beforeUpdateHisExmeReasonCfgs));
                }
				this.beforeUpdateHisExmeReasonCfgs = null;
            }
        }
    }
}
