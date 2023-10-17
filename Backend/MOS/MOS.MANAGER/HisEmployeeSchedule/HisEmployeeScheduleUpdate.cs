using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisEmployeeSchedule
{
    partial class HisEmployeeScheduleUpdate : BusinessBase
    {
		private List<HIS_EMPLOYEE_SCHEDULE> beforeUpdateHisEmployeeSchedules = new List<HIS_EMPLOYEE_SCHEDULE>();
		
        internal HisEmployeeScheduleUpdate()
            : base()
        {

        }

        internal HisEmployeeScheduleUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_EMPLOYEE_SCHEDULE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEmployeeScheduleCheck checker = new HisEmployeeScheduleCheck(param);
                List<HIS_EMPLOYEE_SCHEDULE> before = new HisEmployeeScheduleGet().Get(new HisEmployeeScheduleFilterQuery());
                valid = valid && checker.VerifyRequireField(data);
                HIS_EMPLOYEE_SCHEDULE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.VerifyValid(before, data);
                if (valid)
                {                    
					if (!DAOWorker.HisEmployeeScheduleDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEmployeeSchedule_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisEmployeeSchedule that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisEmployeeSchedules.Add(raw);
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

        internal bool UpdateList(List<HIS_EMPLOYEE_SCHEDULE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisEmployeeScheduleCheck checker = new HisEmployeeScheduleCheck(param);
                List<HIS_EMPLOYEE_SCHEDULE> listRaw = new List<HIS_EMPLOYEE_SCHEDULE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
               
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.VerifyValid(listRaw, data);
                }
                if (valid)
                {
					if (!DAOWorker.HisEmployeeScheduleDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEmployeeSchedule_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisEmployeeSchedule that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisEmployeeSchedules.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisEmployeeSchedules))
            {
                if (!DAOWorker.HisEmployeeScheduleDAO.UpdateList(this.beforeUpdateHisEmployeeSchedules))
                {
                    LogSystem.Warn("Rollback du lieu HisEmployeeSchedule that bai, can kiem tra lai." + LogUtil.TraceData("HisEmployeeSchedules", this.beforeUpdateHisEmployeeSchedules));
                }
				this.beforeUpdateHisEmployeeSchedules = null;
            }
        }
    }
}
