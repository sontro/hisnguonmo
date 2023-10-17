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
	partial class HisPtttCalendarUnapprove : BusinessBase
	{
		private HisPtttCalendarUpdate hisPtttCalendarUpdate;
		
		internal HisPtttCalendarUnapprove()
			: base()
		{
			this.hisPtttCalendarUpdate = new HisPtttCalendarUpdate(param);
		}

		internal HisPtttCalendarUnapprove(CommonParam paramUpdate)
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
				valid = valid && checker.HasWorkPlaceInfo(data.WorkingRoomId, ref workPlace);
				valid = valid && checker.VerifyId(data.Id.Value, ref calendar);
				if (valid)
				{
					if (calendar.APPROVAL_TIME.HasValue || !string.IsNullOrWhiteSpace(calendar.APPROVAL_LOGINNAME) || !string.IsNullOrWhiteSpace(calendar.APPROVAL_LOGINNAME))
					{
						Mapper.CreateMap<HIS_PTTT_CALENDAR, HIS_PTTT_CALENDAR>();
						HIS_PTTT_CALENDAR before = Mapper.Map<HIS_PTTT_CALENDAR>(calendar);
						calendar.APPROVAL_TIME = null;
						calendar.APPROVAL_LOGINNAME = null;
						calendar.APPROVAL_USERNAME = null;
						if (this.hisPtttCalendarUpdate.Update(calendar, before))
						{
							result = true;
							resultData = calendar;
						}
					}
					else
					{
						result = true;
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
