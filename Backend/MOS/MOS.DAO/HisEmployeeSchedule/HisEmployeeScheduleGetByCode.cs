using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEmployeeSchedule
{
    partial class HisEmployeeScheduleGet : EntityBase
    {
        public HIS_EMPLOYEE_SCHEDULE GetByCode(string code, HisEmployeeScheduleSO search)
        {
            HIS_EMPLOYEE_SCHEDULE result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_EMPLOYEE_SCHEDULE.AsQueryable().Where(p => p.EMPLOYEE_SCHEDULE_CODE == code);
                        if (search.listHisEmployeeScheduleExpression != null && search.listHisEmployeeScheduleExpression.Count > 0)
                        {
                            foreach (var item in search.listHisEmployeeScheduleExpression)
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
