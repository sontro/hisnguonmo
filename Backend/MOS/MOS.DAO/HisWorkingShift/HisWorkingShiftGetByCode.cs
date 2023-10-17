using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisWorkingShift
{
    partial class HisWorkingShiftGet : EntityBase
    {
        public HIS_WORKING_SHIFT GetByCode(string code, HisWorkingShiftSO search)
        {
            HIS_WORKING_SHIFT result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_WORKING_SHIFT.AsQueryable().Where(p => p.WORKING_SHIFT_CODE == code);
                        if (search.listHisWorkingShiftExpression != null && search.listHisWorkingShiftExpression.Count > 0)
                        {
                            foreach (var item in search.listHisWorkingShiftExpression)
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
