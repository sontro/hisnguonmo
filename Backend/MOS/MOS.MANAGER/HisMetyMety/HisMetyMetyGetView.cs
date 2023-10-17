using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMetyMety
{
    partial class HisMetyMetyGet : BusinessBase
    {
        internal List<V_HIS_METY_METY> GetView(HisMetyMetyViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMetyMetyDAO.GetView(filter.Query(), param);
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
