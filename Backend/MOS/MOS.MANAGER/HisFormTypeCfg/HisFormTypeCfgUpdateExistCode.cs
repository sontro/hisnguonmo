using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisFormTypeCfg
{
    partial class HisFormTypeCfgUpdate : BusinessBase
    {
		private List<HIS_FORM_TYPE_CFG> beforeUpdateHisFormTypeCfgs = new List<HIS_FORM_TYPE_CFG>();
		
        internal HisFormTypeCfgUpdate()
            : base()
        {

        }

        internal HisFormTypeCfgUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_FORM_TYPE_CFG data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisFormTypeCfgCheck checker = new HisFormTypeCfgCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_FORM_TYPE_CFG raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.FORM_TYPE_CFG_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisFormTypeCfgDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisFormTypeCfg_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisFormTypeCfg that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisFormTypeCfgs.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_FORM_TYPE_CFG> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisFormTypeCfgCheck checker = new HisFormTypeCfgCheck(param);
                List<HIS_FORM_TYPE_CFG> listRaw = new List<HIS_FORM_TYPE_CFG>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.FORM_TYPE_CFG_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisFormTypeCfgDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisFormTypeCfg_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisFormTypeCfg that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisFormTypeCfgs.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisFormTypeCfgs))
            {
                if (!DAOWorker.HisFormTypeCfgDAO.UpdateList(this.beforeUpdateHisFormTypeCfgs))
                {
                    LogSystem.Warn("Rollback du lieu HisFormTypeCfg that bai, can kiem tra lai." + LogUtil.TraceData("HisFormTypeCfgs", this.beforeUpdateHisFormTypeCfgs));
                }
				this.beforeUpdateHisFormTypeCfgs = null;
            }
        }
    }
}
