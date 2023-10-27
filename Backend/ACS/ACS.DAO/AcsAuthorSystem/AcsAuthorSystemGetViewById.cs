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
        public V_ACS_AUTHOR_SYSTEM GetViewById(long id, AcsAuthorSystemSO search)
        {
            V_ACS_AUTHOR_SYSTEM result = null;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_ACS_AUTHOR_SYSTEM.AsQueryable().Where(p => p.ID == id);
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
                Logging(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => id), id) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => search), search), LogType.Error);
                LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
