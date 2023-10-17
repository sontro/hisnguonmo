using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTransfusionSum
{
    partial class HisTransfusionSumGet : BusinessBase
    {
        internal V_HIS_TRANSFUSION_SUM GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisTransfusionSumViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_TRANSFUSION_SUM GetViewByCode(string code, HisTransfusionSumViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTransfusionSumDAO.GetViewByCode(code, filter.Query());
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
