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
        public SDA_CUSTOMIZE_UI GetByCode(string code, SdaCustomizeUiSO search)
        {
            SDA_CUSTOMIZE_UI result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.SDA_CUSTOMIZE_UI.AsQueryable().Where(p => p.CUSTOMIZE_UI_CODE == code);
                        if (search.listSdaCustomizeUiExpression != null && search.listSdaCustomizeUiExpression.Count > 0)
                        {
                            foreach (var item in search.listSdaCustomizeUiExpression)
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
