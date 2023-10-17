using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPtttHighTech
{
    partial class HisPtttHighTechGet : EntityBase
    {
        public HIS_PTTT_HIGH_TECH GetByCode(string code, HisPtttHighTechSO search)
        {
            HIS_PTTT_HIGH_TECH result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_PTTT_HIGH_TECH.AsQueryable().Where(p => p.PTTT_HIGH_TECH_CODE == code);
                        if (search.listHisPtttHighTechExpression != null && search.listHisPtttHighTechExpression.Count > 0)
                        {
                            foreach (var item in search.listHisPtttHighTechExpression)
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
