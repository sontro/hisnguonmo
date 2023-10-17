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
        internal V_HIS_PAAN_LIQUID GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisPaanLiquidViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_PAAN_LIQUID GetViewByCode(string code, HisPaanLiquidViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPaanLiquidDAO.GetViewByCode(code, filter.Query());
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
