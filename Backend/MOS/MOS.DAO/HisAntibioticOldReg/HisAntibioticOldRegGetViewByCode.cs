using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAntibioticOldReg
{
    partial class HisAntibioticOldRegGet : EntityBase
    {
        public V_HIS_ANTIBIOTIC_OLD_REG GetViewByCode(string code, HisAntibioticOldRegSO search)
        {
            V_HIS_ANTIBIOTIC_OLD_REG result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_ANTIBIOTIC_OLD_REG.AsQueryable().Where(p => p.ANTIBIOTIC_OLD_REG_CODE == code);
                        if (search.listVHisAntibioticOldRegExpression != null && search.listVHisAntibioticOldRegExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisAntibioticOldRegExpression)
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
