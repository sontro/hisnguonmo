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
        public SDA_PROVINCE_MAP GetByCode(string code, SdaProvinceMapSO search)
        {
            SDA_PROVINCE_MAP result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.SDA_PROVINCE_MAP.AsQueryable().Where(p => p.PROVINCE_MAP_CODE == code);
                        if (search.listSdaProvinceMapExpression != null && search.listSdaProvinceMapExpression.Count > 0)
                        {
                            foreach (var item in search.listSdaProvinceMapExpression)
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
