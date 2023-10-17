using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisImpMestTypeUser
{
    partial class HisImpMestTypeUserGet : EntityBase
    {
        public HIS_IMP_MEST_TYPE_USER GetByCode(string code, HisImpMestTypeUserSO search)
        {
            HIS_IMP_MEST_TYPE_USER result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_IMP_MEST_TYPE_USER.AsQueryable().Where(p => p.IMP_MEST_TYPE_USER_CODE == code);
                        if (search.listHisImpMestTypeUserExpression != null && search.listHisImpMestTypeUserExpression.Count > 0)
                        {
                            foreach (var item in search.listHisImpMestTypeUserExpression)
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
