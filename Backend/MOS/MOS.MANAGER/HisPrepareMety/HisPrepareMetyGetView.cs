using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPrepareMety
{
    partial class HisPrepareMetyGet : BusinessBase
    {
        internal List<V_HIS_PREPARE_METY> GetView(HisPrepareMetyViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPrepareMetyDAO.GetView(filter.Query(), param);
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
