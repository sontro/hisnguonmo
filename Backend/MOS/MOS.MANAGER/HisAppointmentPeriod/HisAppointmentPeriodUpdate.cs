using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAppointmentPeriod
{
    partial class HisAppointmentPeriodUpdate : BusinessBase
    {
		private List<HIS_APPOINTMENT_PERIOD> beforeUpdateHisAppointmentPeriods = new List<HIS_APPOINTMENT_PERIOD>();
		
        internal HisAppointmentPeriodUpdate()
            : base()
        {

        }

        internal HisAppointmentPeriodUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_APPOINTMENT_PERIOD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAppointmentPeriodCheck checker = new HisAppointmentPeriodCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_APPOINTMENT_PERIOD raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisAppointmentPeriodDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAppointmentPeriod_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAppointmentPeriod that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisAppointmentPeriods.Add(raw);
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

        internal bool UpdateList(List<HIS_APPOINTMENT_PERIOD> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAppointmentPeriodCheck checker = new HisAppointmentPeriodCheck(param);
                List<HIS_APPOINTMENT_PERIOD> listRaw = new List<HIS_APPOINTMENT_PERIOD>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisAppointmentPeriodDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAppointmentPeriod_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAppointmentPeriod that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisAppointmentPeriods.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisAppointmentPeriods))
            {
                if (!DAOWorker.HisAppointmentPeriodDAO.UpdateList(this.beforeUpdateHisAppointmentPeriods))
                {
                    LogSystem.Warn("Rollback du lieu HisAppointmentPeriod that bai, can kiem tra lai." + LogUtil.TraceData("HisAppointmentPeriods", this.beforeUpdateHisAppointmentPeriods));
                }
				this.beforeUpdateHisAppointmentPeriods = null;
            }
        }
    }
}
