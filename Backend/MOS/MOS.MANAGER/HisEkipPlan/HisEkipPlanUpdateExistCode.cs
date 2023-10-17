using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisEkipPlan
{
    partial class HisEkipPlanUpdate : BusinessBase
    {
		private List<HIS_EKIP_PLAN> beforeUpdateHisEkipPlans = new List<HIS_EKIP_PLAN>();
		
        internal HisEkipPlanUpdate()
            : base()
        {

        }

        internal HisEkipPlanUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_EKIP_PLAN data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEkipPlanCheck checker = new HisEkipPlanCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_EKIP_PLAN raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.EKIP_PLAN_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisEkipPlanDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEkipPlan_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisEkipPlan that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisEkipPlans.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_EKIP_PLAN> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisEkipPlanCheck checker = new HisEkipPlanCheck(param);
                List<HIS_EKIP_PLAN> listRaw = new List<HIS_EKIP_PLAN>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.EKIP_PLAN_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisEkipPlanDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEkipPlan_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisEkipPlan that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisEkipPlans.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisEkipPlans))
            {
                if (!DAOWorker.HisEkipPlanDAO.UpdateList(this.beforeUpdateHisEkipPlans))
                {
                    LogSystem.Warn("Rollback du lieu HisEkipPlan that bai, can kiem tra lai." + LogUtil.TraceData("HisEkipPlans", this.beforeUpdateHisEkipPlans));
                }
				this.beforeUpdateHisEkipPlans = null;
            }
        }
    }
}
