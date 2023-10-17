using SDA.DAO.Base;
using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaTranslate
{
    partial class SdaTranslateGet : EntityBase
    {
        public SDA_TRANSLATE GetByCode(string code, SdaTranslateSO search)
        {
            SDA_TRANSLATE result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.SDA_TRANSLATE.AsQueryable().Where(p => p.TRANSLATE_CODE == code);
                        if (search.listSdaTranslateExpression != null && search.listSdaTranslateExpression.Count > 0)
                        {
                            foreach (var item in search.listSdaTranslateExpression)
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
