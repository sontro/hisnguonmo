using ACS.DAO.Base;
using ACS.DAO.StagingObject;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.DAO.AcsRoleAuthor
{
    partial class AcsRoleAuthorGet : EntityBase
    {
        public ACS_ROLE_AUTHOR GetByCode(string code, AcsRoleAuthorSO search)
        {
            ACS_ROLE_AUTHOR result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.ACS_ROLE_AUTHOR.AsQueryable().Where(p => p.ROLE_AUTHOR_CODE == code);
                        if (search.listAcsRoleAuthorExpression != null && search.listAcsRoleAuthorExpression.Count > 0)
                        {
                            foreach (var item in search.listAcsRoleAuthorExpression)
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