using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisPtttCalendar
{
    class HisPtttCalendarTruncateSdo : BusinessBase
	{
		private HisPtttCalendarTruncate hisPtttCalendarTruncate;
		
		internal HisPtttCalendarTruncateSdo()
			: base()
		{
            this.hisPtttCalendarTruncate = new HisPtttCalendarTruncate(param);
		}

        internal HisPtttCalendarTruncateSdo(CommonParam paramUpdate)
			: base(paramUpdate)
		{
            this.hisPtttCalendarTruncate = new  HisPtttCalendarTruncate(param);
		}

		internal bool Run(HisPtttCalendarSDO data)
		{
			bool result = false;
			try
			{
				bool valid = true;
				WorkPlaceSDO workPlace = null;
				HIS_PTTT_CALENDAR raw = null;
				HisPtttCalendarCheck checker = new HisPtttCalendarCheck(param);
				valid = valid && checker.HasWorkPlaceInfo(data.WorkingRoomId, ref workPlace);
                valid = valid && checker.VerifyId(data.Id.Value, ref raw);
                valid = valid && checker.IsNotApproved(raw);
				if (valid)
				{
					Mapper.CreateMap<HIS_PTTT_CALENDAR, HIS_PTTT_CALENDAR>();
                    HIS_PTTT_CALENDAR before = Mapper.Map<HIS_PTTT_CALENDAR>(raw);
                    if (this.hisPtttCalendarTruncate.Truncate(raw, before))
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
