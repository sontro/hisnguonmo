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
        internal HIS_INFUSION_SUM GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisInfusionSumFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_INFUSION_SUM GetByCode(string code, HisInfusionSumFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisInfusionSumDAO.GetByCode(code, filter.Query());
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
