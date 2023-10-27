using ACS.DAO.Base;
using ACS.DAO.StagingObject;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.DAO.AcsAuthorSystem
{
    partial class AcsAuthorSystemGet : EntityBase
    {
        public V_ACS_AUTHOR_SYSTEM GetViewByCode(string code, AcsAuthorSystemSO search)
        {
            V_ACS_AUTHOR_SYSTEM result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_ACS_AUTHOR_SYSTEM.AsQueryable().Where(p => p.AUTHOR_SYSTEM_CODE == code);
                        if (search.listVAcsAuthorSystemExpression != null && search.listVAcsAuthorSystemExpression.Count > 0)
                        {
                            foreach (var item in search.listVAcsAuthorSystemExpression)
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
