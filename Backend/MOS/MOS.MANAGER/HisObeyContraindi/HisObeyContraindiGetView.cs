using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisObeyContraindi
{
    partial class HisObeyContraindiGet : BusinessBase
    {
        internal List<V_HIS_OBEY_CONTRAINDI> GetView(HisObeyContraindiViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisObeyContraindiDAO.GetView(filter.Query(), param);
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
