using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisInfusionSum
{
    partial class HisInfusionSumGet : BusinessBase
    {
        internal V_HIS_INFUSION_SUM GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisInfusionSumViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_INFUSION_SUM GetViewByCode(string code, HisInfusionSumViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisInfusionSumDAO.GetViewByCode(code, filter.Query());
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
