using SDA.DAO.Base;
using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaProvinceMap
{
    partial class SdaProvinceMapGet : EntityBase
    {
        public V_SDA_PROVINCE_MAP GetViewById(long id, SdaProvinceMapSO search)
        {
            V_SDA_PROVINCE_MAP result = null;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_SDA_PROVINCE_MAP.AsQueryable().Where(p => p.ID == id);
                        if (search.listVSdaProvinceMapExpression != null && search.listVSdaProvinceMapExpression.Count > 0)
                        {
                            foreach (var item in search.listVSdaProvinceMapExpression)
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
