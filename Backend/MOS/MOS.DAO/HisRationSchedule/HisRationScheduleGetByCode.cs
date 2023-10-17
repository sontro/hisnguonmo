using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRationSchedule
{
    partial class HisRationScheduleGet : EntityBase
    {
        public HIS_RATION_SCHEDULE GetByCode(string code, HisRationScheduleSO search)
        {
            HIS_RATION_SCHEDULE result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_RATION_SCHEDULE.AsQueryable().Where(p => p.RATION_SCHEDULE_CODE == code);
                        if (search.listHisRationScheduleExpression != null && search.listHisRationScheduleExpression.Count > 0)
                        {
                            foreach (var item in search.listHisRationScheduleExpression)
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
