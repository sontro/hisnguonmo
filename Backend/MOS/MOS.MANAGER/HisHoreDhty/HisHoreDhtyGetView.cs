using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHoreDhty
{
    partial class HisHoreDhtyGet : BusinessBase
    {
        internal List<V_HIS_HORE_DHTY> GetView(HisHoreDhtyViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisHoreDhtyDAO.GetView(filter.Query(), param);
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
