using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRegimenHiv
{
    partial class HisRegimenHivGet : EntityBase
    {
        public HIS_REGIMEN_HIV GetByCode(string code, HisRegimenHivSO search)
        {
            HIS_REGIMEN_HIV result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_REGIMEN_HIV.AsQueryable().Where(p => p.REGIMEN_HIV_CODE == code);
                        if (search.listHisRegimenHivExpression != null && search.listHisRegimenHivExpression.Count > 0)
                        {
                            foreach (var item in search.listHisRegimenHivExpression)
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
