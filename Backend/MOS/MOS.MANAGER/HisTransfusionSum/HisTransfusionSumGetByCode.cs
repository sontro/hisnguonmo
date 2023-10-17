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
        internal HIS_TRANSFUSION_SUM GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisTransfusionSumFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_TRANSFUSION_SUM GetByCode(string code, HisTransfusionSumFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTransfusionSumDAO.GetByCode(code, filter.Query());
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
