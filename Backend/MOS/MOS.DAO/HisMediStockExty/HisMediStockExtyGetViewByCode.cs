using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMediStockExty
{
    partial class HisMediStockExtyGet : EntityBase
    {
        public V_HIS_MEDI_STOCK_EXTY GetViewByCode(string code, HisMediStockExtySO search)
        {
            V_HIS_MEDI_STOCK_EXTY result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_MEDI_STOCK_EXTY.AsQueryable().Where(p => p.MEDI_STOCK_EXTY_CODE == code);
                        if (search.listVHisMediStockExtyExpression != null && search.listVHisMediStockExtyExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisMediStockExtyExpression)
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
