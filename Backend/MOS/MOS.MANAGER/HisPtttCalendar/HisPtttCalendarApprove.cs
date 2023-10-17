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
	partial class HisPtttCalendarApprove : BusinessBase
	{
		private HisPtttCalendarUpdate hisPtttCalendarUpdate;
		
		internal HisPtttCalendarApprove()
			: base()
		{
			this.hisPtttCalendarUpdate = new HisPtttCalendarUpdate(param);
		}

		internal HisPtttCalendarApprove(CommonParam paramUpdate)
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
				valid = valid && checker.IsNotApproved(calendar);
				if (valid)
				{
					Mapper.CreateMap<HIS_PTTT_CALENDAR, HIS_PTTT_CALENDAR>();
					HIS_PTTT_CALENDAR before = Mapper.Map<HIS_PTTT_CALENDAR>(calendar);
					calendar.APPROVAL_TIME = Inventec.Common.DateTime.Get.Now();
					calendar.APPROVAL_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
					calendar.APPROVAL_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
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
