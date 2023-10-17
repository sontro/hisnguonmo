using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExecuteRoleUser
{
    partial class HisExecuteRoleUserGet : EntityBase
    {
        public V_HIS_EXECUTE_ROLE_USER GetViewByCode(string code, HisExecuteRoleUserSO search)
        {
            V_HIS_EXECUTE_ROLE_USER result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_EXECUTE_ROLE_USER.AsQueryable().Where(p => p.EXECUTE_ROLE_USER_CODE == code);
                        if (search.listVHisExecuteRoleUserExpression != null && search.listVHisExecuteRoleUserExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisExecuteRoleUserExpression)
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
