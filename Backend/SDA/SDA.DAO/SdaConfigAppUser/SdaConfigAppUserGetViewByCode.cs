using SDA.DAO.Base;
using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaConfigAppUser
{
    partial class SdaConfigAppUserGet : EntityBase
    {
        public V_SDA_CONFIG_APP_USER GetViewByCode(string code, SdaConfigAppUserSO search)
        {
            V_SDA_CONFIG_APP_USER result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_SDA_CONFIG_APP_USER.AsQueryable().Where(p => p.CONFIG_APP_USER_CODE == code);
                        if (search.listVSdaConfigAppUserExpression != null && search.listVSdaConfigAppUserExpression.Count > 0)
                        {
                            foreach (var item in search.listVSdaConfigAppUserExpression)
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
