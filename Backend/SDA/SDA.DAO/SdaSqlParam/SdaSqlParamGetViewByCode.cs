using SDA.DAO.Base;
using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaSqlParam
{
    partial class SdaSqlParamGet : EntityBase
    {
        public V_SDA_SQL_PARAM GetViewByCode(string code, SdaSqlParamSO search)
        {
            V_SDA_SQL_PARAM result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_SDA_SQL_PARAM.AsQueryable().Where(p => p.SQL_PARAM_CODE == code);
                        if (search.listVSdaSqlParamExpression != null && search.listVSdaSqlParamExpression.Count > 0)
                        {
                            foreach (var item in search.listVSdaSqlParamExpression)
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
