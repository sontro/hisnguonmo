using SAR.DAO.Base;
using SAR.DAO.StagingObject;
using SAR.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAR.DAO.SarRetyFofi
{
    partial class SarRetyFofiGet : EntityBase
    {
        public V_SAR_RETY_FOFI GetViewByCode(string code, SarRetyFofiSO search)
        {
            V_SAR_RETY_FOFI result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_SAR_RETY_FOFI.AsQueryable().Where(p => p.RETY_FOFI_CODE == code);
                        if (search.listVSarRetyFofiExpression != null && search.listVSarRetyFofiExpression.Count > 0)
                        {
                            foreach (var item in search.listVSarRetyFofiExpression)
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
