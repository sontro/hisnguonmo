using ACS.DAO.Base;
using ACS.DAO.StagingObject;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.DAO.AcsModuleRole
{
    partial class AcsModuleRoleGet : EntityBase
    {
        public V_ACS_MODULE_ROLE GetViewByCode(string code, AcsModuleRoleSO search)
        {
            V_ACS_MODULE_ROLE result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_ACS_MODULE_ROLE.AsQueryable().Where(p => p.MODULE_ROLE_CODE == code);
                        if (search.listVAcsModuleRoleExpression != null && search.listVAcsModuleRoleExpression.Count > 0)
                        {
                            foreach (var item in search.listVAcsModuleRoleExpression)
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
