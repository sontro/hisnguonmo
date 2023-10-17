using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRationSchedule
{
    partial class HisRationScheduleCreate : BusinessBase
    {
		private List<HIS_RATION_SCHEDULE> recentHisRationSchedules = new List<HIS_RATION_SCHEDULE>();
		
        internal HisRationScheduleCreate()
            : base()
        {

        }

        internal HisRationScheduleCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_RATION_SCHEDULE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRationScheduleCheck checker = new HisRationScheduleCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.RATION_SCHEDULE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisRationScheduleDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRationSchedule_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisRationSchedule that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisRationSchedules.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisRationSchedules))
            {
                if (!DAOWorker.HisRationScheduleDAO.TruncateList(this.recentHisRationSchedules))
                {
                    LogSystem.Warn("Rollback du lieu HisRationSchedule that bai, can kiem tra lai." + LogUtil.TraceData("recentHisRationSchedules", this.recentHisRationSchedules));
                }
				this.recentHisRationSchedules = null;
            }
        }
    }
}
