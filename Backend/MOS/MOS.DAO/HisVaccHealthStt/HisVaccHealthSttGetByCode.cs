using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisVaccHealthStt
{
    partial class HisVaccHealthSttGet : EntityBase
    {
        public HIS_VACC_HEALTH_STT GetByCode(string code, HisVaccHealthSttSO search)
        {
            HIS_VACC_HEALTH_STT result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_VACC_HEALTH_STT.AsQueryable().Where(p => p.VACC_HEALTH_STT_CODE == code);
                        if (search.listHisVaccHealthSttExpression != null && search.listHisVaccHealthSttExpression.Count > 0)
                        {
                            foreach (var item in search.listHisVaccHealthSttExpression)
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
