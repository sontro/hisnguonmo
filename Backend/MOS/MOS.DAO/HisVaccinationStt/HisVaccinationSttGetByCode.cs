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
        public HIS_VACCINATION_STT GetByCode(string code, HisVaccinationSttSO search)
        {
            HIS_VACCINATION_STT result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_VACCINATION_STT.AsQueryable().Where(p => p.VACCINATION_STT_CODE == code);
                        if (search.listHisVaccinationSttExpression != null && search.listHisVaccinationSttExpression.Count > 0)
                        {
                            foreach (var item in search.listHisVaccinationSttExpression)
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
