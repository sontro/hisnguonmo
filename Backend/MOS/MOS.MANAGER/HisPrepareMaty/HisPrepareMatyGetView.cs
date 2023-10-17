using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPrepareMaty
{
    partial class HisPrepareMatyGet : BusinessBase
    {
        internal List<V_HIS_PREPARE_MATY> GetView(HisPrepareMatyViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPrepareMatyDAO.GetView(filter.Query(), param);
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
