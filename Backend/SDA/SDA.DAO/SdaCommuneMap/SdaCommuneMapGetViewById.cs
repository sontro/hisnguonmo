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
        public V_SDA_COMMUNE_MAP GetViewById(long id, SdaCommuneMapSO search)
        {
            V_SDA_COMMUNE_MAP result = null;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_SDA_COMMUNE_MAP.AsQueryable().Where(p => p.ID == id);
                        if (search.listVSdaCommuneMapExpression != null && search.listVSdaCommuneMapExpression.Count > 0)
                        {
                            foreach (var item in search.listVSdaCommuneMapExpression)
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
