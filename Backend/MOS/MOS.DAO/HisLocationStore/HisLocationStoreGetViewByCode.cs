using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisLocationStore
{
    partial class HisLocationStoreGet : EntityBase
    {
        public V_HIS_LOCATION_STORE GetViewByCode(string code, HisLocationStoreSO search)
        {
            V_HIS_LOCATION_STORE result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_LOCATION_STORE.AsQueryable().Where(p => p.LOCATION_STORE_CODE == code);
                        if (search.listVHisLocationStoreExpression != null && search.listVHisLocationStoreExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisLocationStoreExpression)
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
