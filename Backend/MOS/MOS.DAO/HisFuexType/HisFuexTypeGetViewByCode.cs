using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisFuexType
{
    partial class HisFuexTypeGet : EntityBase
    {
        public V_HIS_FUEX_TYPE GetViewByCode(string code, HisFuexTypeSO search)
        {
            V_HIS_FUEX_TYPE result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_FUEX_TYPE.AsQueryable().Where(p => p.FUEX_TYPE_CODE == code);
                        if (search.listVHisFuexTypeExpression != null && search.listVHisFuexTypeExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisFuexTypeExpression)
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
