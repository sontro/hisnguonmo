using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExecuteRole
{
    partial class HisExecuteRoleGet : EntityBase
    {
        public V_HIS_EXECUTE_ROLE GetViewByCode(string code, HisExecuteRoleSO search)
        {
            V_HIS_EXECUTE_ROLE result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_EXECUTE_ROLE.AsQueryable().Where(p => p.EXECUTE_ROLE_CODE == code);
                        if (search.listVHisExecuteRoleExpression != null && search.listVHisExecuteRoleExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisExecuteRoleExpression)
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
