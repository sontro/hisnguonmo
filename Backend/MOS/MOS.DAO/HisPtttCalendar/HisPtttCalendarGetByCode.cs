using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPtttCalendar
{
    partial class HisPtttCalendarGet : EntityBase
    {
        public HIS_PTTT_CALENDAR GetByCode(string code, HisPtttCalendarSO search)
        {
            HIS_PTTT_CALENDAR result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_PTTT_CALENDAR.AsQueryable().Where(p => p.PTTT_CALENDAR_CODE == code);
                        if (search.listHisPtttCalendarExpression != null && search.listHisPtttCalendarExpression.Count > 0)
                        {
                            foreach (var item in search.listHisPtttCalendarExpression)
                            {
                                query = query.Where(item);
                            }
                        }
                        result = query.SingleOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                Logging(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => code), code) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => search), search), LogType.Error);
                LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
