using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAppointmentPeriod
{
    partial class HisAppointmentPeriodCreate : BusinessBase
    {
		private List<HIS_APPOINTMENT_PERIOD> recentHisAppointmentPeriods = new List<HIS_APPOINTMENT_PERIOD>();
		
        internal HisAppointmentPeriodCreate()
            : base()
        {

        }

        internal HisAppointmentPeriodCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_APPOINTMENT_PERIOD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAppointmentPeriodCheck checker = new HisAppointmentPeriodCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisAppointmentPeriodDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAppointmentPeriod_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAppointmentPeriod that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisAppointmentPeriods.Add(data);
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
		
		internal bool CreateList(List<HIS_APPOINTMENT_PERIOD> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAppointmentPeriodCheck checker = new HisAppointmentPeriodCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisAppointmentPeriodDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAppointmentPeriod_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAppointmentPeriod that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisAppointmentPeriods.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisAppointmentPeriods))
            {
                if (!DAOWorker.HisAppointmentPeriodDAO.TruncateList(this.recentHisAppointmentPeriods))
                {
                    LogSystem.Warn("Rollback du lieu HisAppointmentPeriod that bai, can kiem tra lai." + LogUtil.TraceData("recentHisAppointmentPeriods", this.recentHisAppointmentPeriods));
                }
				this.recentHisAppointmentPeriods = null;
            }
        }
    }
}
