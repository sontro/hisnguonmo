using SDA.DAO.Base;
using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaCommuneMap
{
    partial class SdaCommuneMapGet : EntityBase
    {
        public SDA_COMMUNE_MAP GetByCode(string code, SdaCommuneMapSO search)
        {
            SDA_COMMUNE_MAP result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.SDA_COMMUNE_MAP.AsQueryable().Where(p => p.COMMUNE_MAP_CODE == code);
                        if (search.listSdaCommuneMapExpression != null && search.listSdaCommuneMapExpression.Count > 0)
                        {
                            foreach (var item in search.listSdaCommuneMapExpression)
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
