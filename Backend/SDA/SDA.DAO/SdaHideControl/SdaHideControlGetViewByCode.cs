using SDA.DAO.Base;
using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaHideControl
{
    partial class SdaHideControlGet : EntityBase
    {
        public V_SDA_HIDE_CONTROL GetViewByCode(string code, SdaHideControlSO search)
        {
            V_SDA_HIDE_CONTROL result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_SDA_HIDE_CONTROL.AsQueryable().Where(p => p.HIDE_CONTROL_CODE == code);
                        if (search.listVSdaHideControlExpression != null && search.listVSdaHideControlExpression.Count > 0)
                        {
                            foreach (var item in search.listVSdaHideControlExpression)
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
