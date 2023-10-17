using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMetyMaty
{
    partial class HisMetyMatyGet : BusinessBase
    {
        internal List<V_HIS_METY_MATY> GetView(HisMetyMatyViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMetyMatyDAO.GetView(filter.Query(), param);
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
