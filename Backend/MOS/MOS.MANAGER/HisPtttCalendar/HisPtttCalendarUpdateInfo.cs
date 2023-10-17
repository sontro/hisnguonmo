using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPtttCalendar
{
    partial class HisPtttCalendarUpdateInfo : BusinessBase
    {
        private HisPtttCalendarUpdate hisPtttCalendarUpdate;
		
        internal HisPtttCalendarUpdateInfo()
            : base()
        {
            this.hisPtttCalendarUpdate = new HisPtttCalendarUpdate(param);
        }

        internal HisPtttCalendarUpdateInfo(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.hisPtttCalendarUpdate = new HisPtttCalendarUpdate(param);
        }

        internal bool Run(HisPtttCalendarSDO data, ref HIS_PTTT_CALENDAR resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                WorkPlaceSDO workPlace = null;
                HIS_PTTT_CALENDAR calendar = null;
                HisPtttCalendarCheck checker = new HisPtttCalendarCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.HasWorkPlaceInfo(data.WorkingRoomId, ref workPlace);
                valid = valid && checker.VerifyId(data.Id.Value, ref calendar);
                valid = valid && checker.IsWorkingAtDepartment(calendar.DEPARTMENT_ID, workPlace.DepartmentId);
                valid = valid && checker.IsNotIntersectTime(calendar.ID, workPlace.DepartmentId, data.TimeFrom, data.TimeTo);
                valid = valid && checker.HasNoServiceReqOutOfTime(calendar.ID, data.TimeFrom, data.TimeTo);
                valid = valid && checker.IsNotApproved(calendar);
                if (valid)
                {
                    Mapper.CreateMap<HIS_PTTT_CALENDAR, HIS_PTTT_CALENDAR>();
                    HIS_PTTT_CALENDAR before = Mapper.Map<HIS_PTTT_CALENDAR>(calendar);
                    calendar.TIME_TO = data.TimeTo;
                    calendar.TIME_FROM = data.TimeFrom;
                    if (this.hisPtttCalendarUpdate.Update(calendar, before))
                    {
                        result = true;
                        resultData = calendar;
                    }
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
    }
}
