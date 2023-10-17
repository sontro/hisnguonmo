using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAntibioticNewReg
{
    partial class HisAntibioticNewRegGet : EntityBase
    {
        public HIS_ANTIBIOTIC_NEW_REG GetByCode(string code, HisAntibioticNewRegSO search)
        {
            HIS_ANTIBIOTIC_NEW_REG result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_ANTIBIOTIC_NEW_REG.AsQueryable().Where(p => p.ANTIBIOTIC_NEW_REG_CODE == code);
                        if (search.listHisAntibioticNewRegExpression != null && search.listHisAntibioticNewRegExpression.Count > 0)
                        {
                            foreach (var item in search.listHisAntibioticNewRegExpression)
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
