using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAntigenMety
{
    partial class HisAntigenMetyGet : BusinessBase
    {
        internal List<V_HIS_ANTIGEN_METY> GetView(HisAntigenMetyViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAntigenMetyDAO.GetView(filter.Query(), param);
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
