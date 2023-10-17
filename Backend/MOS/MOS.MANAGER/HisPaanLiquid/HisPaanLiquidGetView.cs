using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPaanLiquid
{
    partial class HisPaanLiquidGet : BusinessBase
    {
        internal List<V_HIS_PAAN_LIQUID> GetView(HisPaanLiquidViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPaanLiquidDAO.GetView(filter.Query(), param);
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
