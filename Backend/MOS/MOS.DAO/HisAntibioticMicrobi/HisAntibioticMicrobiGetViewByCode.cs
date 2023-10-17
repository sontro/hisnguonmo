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
        public V_HIS_ANTIBIOTIC_MICROBI GetViewByCode(string code, HisAntibioticMicrobiSO search)
        {
            V_HIS_ANTIBIOTIC_MICROBI result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_ANTIBIOTIC_MICROBI.AsQueryable().Where(p => p.ANTIBIOTIC_MICROBI_CODE == code);
                        if (search.listVHisAntibioticMicrobiExpression != null && search.listVHisAntibioticMicrobiExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisAntibioticMicrobiExpression)
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
