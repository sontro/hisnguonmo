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
        internal List<V_HIS_TRANSFUSION_SUM> GetView(HisTransfusionSumViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTransfusionSumDAO.GetView(filter.Query(), param);
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
