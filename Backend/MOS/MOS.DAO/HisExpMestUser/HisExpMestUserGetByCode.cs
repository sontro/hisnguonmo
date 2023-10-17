using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExpMestUser
{
    partial class HisExpMestUserGet : EntityBase
    {
        public HIS_EXP_MEST_USER GetByCode(string code, HisExpMestUserSO search)
        {
            HIS_EXP_MEST_USER result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_EXP_MEST_USER.AsQueryable().Where(p => p.EXP_MEST_USER_CODE == code);
                        if (search.listHisExpMestUserExpression != null && search.listHisExpMestUserExpression.Count > 0)
                        {
                            foreach (var item in search.listHisExpMestUserExpression)
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
