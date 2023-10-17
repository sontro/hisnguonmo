using SDA.DAO.Base;
using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaConfigApp
{
    partial class SdaConfigAppGet : EntityBase
    {
        public V_SDA_CONFIG_APP GetViewByCode(string code, SdaConfigAppSO search)
        {
            V_SDA_CONFIG_APP result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_SDA_CONFIG_APP.AsQueryable().Where(p => p.CONFIG_APP_CODE == code);
                        if (search.listVSdaConfigAppExpression != null && search.listVSdaConfigAppExpression.Count > 0)
                        {
                            foreach (var item in search.listVSdaConfigAppExpression)
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
