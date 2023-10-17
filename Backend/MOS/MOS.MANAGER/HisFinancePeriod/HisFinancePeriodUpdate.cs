using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisFinancePeriod
{
    partial class HisFinancePeriodUpdate : BusinessBase
    {
		private List<HIS_FINANCE_PERIOD> beforeUpdateHisFinancePeriods = new List<HIS_FINANCE_PERIOD>();
		
        internal HisFinancePeriodUpdate()
            : base()
        {

        }

        internal HisFinancePeriodUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_FINANCE_PERIOD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisFinancePeriodCheck checker = new HisFinancePeriodCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_FINANCE_PERIOD raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisFinancePeriodDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisFinancePeriod_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisFinancePeriod that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisFinancePeriods.Add(raw);
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

        internal bool UpdateList(List<HIS_FINANCE_PERIOD> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisFinancePeriodCheck checker = new HisFinancePeriodCheck(param);
                List<HIS_FINANCE_PERIOD> listRaw = new List<HIS_FINANCE_PERIOD>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisFinancePeriodDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisFinancePeriod_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisFinancePeriod that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisFinancePeriods.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisFinancePeriods))
            {
                if (!DAOWorker.HisFinancePeriodDAO.UpdateList(this.beforeUpdateHisFinancePeriods))
                {
                    LogSystem.Warn("Rollback du lieu HisFinancePeriod that bai, can kiem tra lai." + LogUtil.TraceData("HisFinancePeriods", this.beforeUpdateHisFinancePeriods));
                }
				this.beforeUpdateHisFinancePeriods = null;
            }
        }
    }
}
