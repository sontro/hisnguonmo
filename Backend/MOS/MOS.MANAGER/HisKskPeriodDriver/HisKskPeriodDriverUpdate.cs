using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisKskPeriodDriver
{
    partial class HisKskPeriodDriverUpdate : BusinessBase
    {
		private List<HIS_KSK_PERIOD_DRIVER> beforeUpdateHisKskPeriodDrivers = new List<HIS_KSK_PERIOD_DRIVER>();
		
        internal HisKskPeriodDriverUpdate()
            : base()
        {

        }

        internal HisKskPeriodDriverUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_KSK_PERIOD_DRIVER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisKskPeriodDriverCheck checker = new HisKskPeriodDriverCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_KSK_PERIOD_DRIVER raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisKskPeriodDriverDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskPeriodDriver_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisKskPeriodDriver that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisKskPeriodDrivers.Add(raw);
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

        internal bool UpdateList(List<HIS_KSK_PERIOD_DRIVER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisKskPeriodDriverCheck checker = new HisKskPeriodDriverCheck(param);
                List<HIS_KSK_PERIOD_DRIVER> listRaw = new List<HIS_KSK_PERIOD_DRIVER>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisKskPeriodDriverDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskPeriodDriver_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisKskPeriodDriver that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisKskPeriodDrivers.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisKskPeriodDrivers))
            {
                if (!DAOWorker.HisKskPeriodDriverDAO.UpdateList(this.beforeUpdateHisKskPeriodDrivers))
                {
                    LogSystem.Warn("Rollback du lieu HisKskPeriodDriver that bai, can kiem tra lai." + LogUtil.TraceData("HisKskPeriodDrivers", this.beforeUpdateHisKskPeriodDrivers));
                }
				this.beforeUpdateHisKskPeriodDrivers = null;
            }
        }
    }
}
