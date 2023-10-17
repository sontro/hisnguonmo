using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPtttCalendar
{
    partial class HisPtttCalendarDelete : BusinessBase
    {
        internal HisPtttCalendarDelete()
            : base()
        {

        }

        internal HisPtttCalendarDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_PTTT_CALENDAR data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPtttCalendarCheck checker = new HisPtttCalendarCheck(param);
                valid = valid && IsNotNull(data);
                HIS_PTTT_CALENDAR raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisPtttCalendarDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_PTTT_CALENDAR> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPtttCalendarCheck checker = new HisPtttCalendarCheck(param);
                List<HIS_PTTT_CALENDAR> listRaw = new List<HIS_PTTT_CALENDAR>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisPtttCalendarDAO.DeleteList(listData);
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
