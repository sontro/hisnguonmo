using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAntibioticMicrobi
{
    partial class HisAntibioticMicrobiGet : EntityBase
    {
        public HIS_ANTIBIOTIC_MICROBI GetByCode(string code, HisAntibioticMicrobiSO search)
        {
            HIS_ANTIBIOTIC_MICROBI result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_ANTIBIOTIC_MICROBI.AsQueryable().Where(p => p.ANTIBIOTIC_MICROBI_CODE == code);
                        if (search.listHisAntibioticMicrobiExpression != null && search.listHisAntibioticMicrobiExpression.Count > 0)
                        {
                            foreach (var item in search.listHisAntibioticMicrobiExpression)
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
