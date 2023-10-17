using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPtttCalendar
{
	partial class HisPtttCalendarCreate : BusinessBase
	{
		private List<HIS_PTTT_CALENDAR> recentHisPtttCalendars = new List<HIS_PTTT_CALENDAR>();
		
		internal HisPtttCalendarCreate()
			: base()
		{

		}

		internal HisPtttCalendarCreate(CommonParam paramCreate)
			: base(paramCreate)
		{

		}

        internal bool Create(HisPtttCalendarSDO data, ref HIS_PTTT_CALENDAR resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                WorkPlaceSDO workPlace = null;
                HisPtttCalendarCheck checker = new HisPtttCalendarCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.HasWorkPlaceInfo(data.WorkingRoomId, ref workPlace);
                valid = valid && checker.IsNotIntersectTime(0, workPlace.DepartmentId, data.TimeFrom, data.TimeTo);
                if (valid)
                {
                    HIS_PTTT_CALENDAR pc = new HIS_PTTT_CALENDAR();
                    pc.TIME_FROM = data.TimeFrom;
                    pc.TIME_TO = data.TimeTo;
                    pc.DEPARTMENT_ID = workPlace.DepartmentId;

                    if (!DAOWorker.HisPtttCalendarDAO.Create(pc))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPtttCalendar_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPtttCalendar that bai." + LogUtil.TraceData("data", data));
                    }
                    resultData = pc;
                    this.recentHisPtttCalendars.Add(pc);
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
			if (IsNotNullOrEmpty(this.recentHisPtttCalendars))
			{
				if (!DAOWorker.HisPtttCalendarDAO.TruncateList(this.recentHisPtttCalendars))
				{
					LogSystem.Warn("Rollback du lieu HisPtttCalendar that bai, can kiem tra lai." + LogUtil.TraceData("recentHisPtttCalendars", this.recentHisPtttCalendars));
				}
				this.recentHisPtttCalendars = null;
			}
		}
	}
}
