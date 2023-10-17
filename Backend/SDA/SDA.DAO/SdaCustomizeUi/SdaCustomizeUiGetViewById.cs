using SDA.DAO.Base;
using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaCustomizeUi
{
    partial class SdaCustomizeUiGet : EntityBase
    {
        public V_SDA_CUSTOMIZE_UI GetViewById(long id, SdaCustomizeUiSO search)
        {
            V_SDA_CUSTOMIZE_UI result = null;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_SDA_CUSTOMIZE_UI.AsQueryable().Where(p => p.ID == id);
                        if (search.listVSdaCustomizeUiExpression != null && search.listVSdaCustomizeUiExpression.Count > 0)
                        {
                            foreach (var item in search.listVSdaCustomizeUiExpression)
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
