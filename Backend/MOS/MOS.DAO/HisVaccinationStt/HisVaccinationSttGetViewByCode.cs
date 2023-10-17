using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisVaccinationStt
{
    partial class HisVaccinationSttGet : EntityBase
    {
        public V_HIS_VACCINATION_STT GetViewByCode(string code, HisVaccinationSttSO search)
        {
            V_HIS_VACCINATION_STT result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_VACCINATION_STT.AsQueryable().Where(p => p.VACCINATION_STT_CODE == code);
                        if (search.listVHisVaccinationSttExpression != null && search.listVHisVaccinationSttExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisVaccinationSttExpression)
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
