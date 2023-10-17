using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisRationSchedule
{
    partial class HisRationScheduleUpdate : BusinessBase
    {
		private List<HIS_RATION_SCHEDULE> beforeUpdateHisRationSchedules = new List<HIS_RATION_SCHEDULE>();
		
        internal HisRationScheduleUpdate()
            : base()
        {

        }

        internal HisRationScheduleUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_RATION_SCHEDULE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRationScheduleCheck checker = new HisRationScheduleCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_RATION_SCHEDULE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.RATION_SCHEDULE_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisRationScheduleDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRationSchedule_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisRationSchedule that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisRationSchedules.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_RATION_SCHEDULE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRationScheduleCheck checker = new HisRationScheduleCheck(param);
                List<HIS_RATION_SCHEDULE> listRaw = new List<HIS_RATION_SCHEDULE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.RATION_SCHEDULE_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisRationScheduleDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRationSchedule_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisRationSchedule that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisRationSchedules.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisRationSchedules))
            {
                if (!DAOWorker.HisRationScheduleDAO.UpdateList(this.beforeUpdateHisRationSchedules))
                {
                    LogSystem.Warn("Rollback du lieu HisRationSchedule that bai, can kiem tra lai." + LogUtil.TraceData("HisRationSchedules", this.beforeUpdateHisRationSchedules));
                }
				this.beforeUpdateHisRationSchedules = null;
            }
        }
    }
}
