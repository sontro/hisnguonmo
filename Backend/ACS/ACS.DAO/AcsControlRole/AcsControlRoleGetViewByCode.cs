using ACS.DAO.Base;
using ACS.DAO.StagingObject;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.DAO.AcsControlRole
{
    partial class AcsControlRoleGet : EntityBase
    {
        public V_ACS_CONTROL_ROLE GetViewByCode(string code, AcsControlRoleSO search)
        {
            V_ACS_CONTROL_ROLE result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_ACS_CONTROL_ROLE.AsQueryable().Where(p => p.CONTROL_ROLE_CODE == code);
                        if (search.listVAcsControlRoleExpression != null && search.listVAcsControlRoleExpression.Count > 0)
                        {
                            foreach (var item in search.listVAcsControlRoleExpression)
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
