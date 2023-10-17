using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTransfusion
{
    partial class HisTransfusionGet : BusinessBase
    {
        internal List<V_HIS_TRANSFUSION> GetView(HisTransfusionViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTransfusionDAO.GetView(filter.Query(), param);
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
