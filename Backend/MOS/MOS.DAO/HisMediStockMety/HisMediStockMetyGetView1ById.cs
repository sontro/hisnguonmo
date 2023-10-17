using Inventec.Common.Logging;
using Inventec.Core;
using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.DAO.HisMediStockMety
{
    partial class HisMediStockMetyGet : EntityBase
    {
        public V_HIS_MEDI_STOCK_METY_1 GetView1ById(long id, HisMediStockMetySO search)
        {
            V_HIS_MEDI_STOCK_METY_1 result = null;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_MEDI_STOCK_METY_1.AsQueryable().Where(p => p.ID == id);
                        if (search.listVHisMediStockMety1Expression != null && search.listVHisMediStockMety1Expression.Count > 0)
                        {
                            foreach (var item in search.listVHisMediStockMety1Expression)
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
