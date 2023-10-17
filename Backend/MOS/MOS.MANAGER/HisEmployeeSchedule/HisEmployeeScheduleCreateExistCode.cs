using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEmployeeSchedule
{
    partial class HisEmployeeScheduleCreate : BusinessBase
    {
		private List<HIS_EMPLOYEE_SCHEDULE> recentHisEmployeeSchedules = new List<HIS_EMPLOYEE_SCHEDULE>();
		
        internal HisEmployeeScheduleCreate()
            : base()
        {

        }

        internal HisEmployeeScheduleCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_EMPLOYEE_SCHEDULE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEmployeeScheduleCheck checker = new HisEmployeeScheduleCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.EMPLOYEE_SCHEDULE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisEmployeeScheduleDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEmployeeSchedule_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisEmployeeSchedule that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisEmployeeSchedules.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisEmployeeSchedules))
            {
                if (!DAOWorker.HisEmployeeScheduleDAO.TruncateList(this.recentHisEmployeeSchedules))
                {
                    LogSystem.Warn("Rollback du lieu HisEmployeeSchedule that bai, can kiem tra lai." + LogUtil.TraceData("recentHisEmployeeSchedules", this.recentHisEmployeeSchedules));
                }
				this.recentHisEmployeeSchedules = null;
            }
        }
    }
}
