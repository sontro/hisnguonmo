using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMestInventory
{
    partial class HisMestInventoryGet : EntityBase
    {
        public HIS_MEST_INVENTORY GetByCode(string code, HisMestInventorySO search)
        {
            HIS_MEST_INVENTORY result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_MEST_INVENTORY.AsQueryable().Where(p => p.MEST_INVENTORY_CODE == code);
                        if (search.listHisMestInventoryExpression != null && search.listHisMestInventoryExpression.Count > 0)
                        {
                            foreach (var item in search.listHisMestInventoryExpression)
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
