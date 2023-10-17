using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediStockImty
{
    partial class HisMediStockImtyGet : BusinessBase
    {
        internal List<V_HIS_MEDI_STOCK_IMTY> GetView(HisMediStockImtyViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMediStockImtyDAO.GetView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
