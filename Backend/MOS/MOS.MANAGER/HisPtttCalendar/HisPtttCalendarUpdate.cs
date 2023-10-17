using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPtttCalendar
{
    partial class HisPtttCalendarUpdate : BusinessBase
    {
		private List<HIS_PTTT_CALENDAR> beforeUpdateHisPtttCalendars = new List<HIS_PTTT_CALENDAR>();
		
        internal HisPtttCalendarUpdate()
            : base()
        {

        }

        internal HisPtttCalendarUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_PTTT_CALENDAR data)
        {
            HIS_PTTT_CALENDAR before = new HisPtttCalendarGet().GetById(data.ID);
            return this.Update(data, before);
        }

        internal bool Update(HIS_PTTT_CALENDAR data, HIS_PTTT_CALENDAR before)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPtttCalendarCheck checker = new HisPtttCalendarCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsUnLock(before);
                if (valid)
                {
                    if (!DAOWorker.HisPtttCalendarDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPtttCalendar_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPtttCalendar that bai." + LogUtil.TraceData("data", data));
                    }

                    this.beforeUpdateHisPtttCalendars.Add(before);

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
            if (IsNotNullOrEmpty(this.beforeUpdateHisPtttCalendars))
            {
                if (!DAOWorker.HisPtttCalendarDAO.UpdateList(this.beforeUpdateHisPtttCalendars))
                {
                    LogSystem.Warn("Rollback du lieu HisPtttCalendar that bai, can kiem tra lai." + LogUtil.TraceData("HisPtttCalendars", this.beforeUpdateHisPtttCalendars));
                }
				this.beforeUpdateHisPtttCalendars = null;
            }
        }
    }
}
