using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEmrCoverConfig
{
    partial class HisEmrCoverConfigGet : BusinessBase
    {
        internal List<V_HIS_EMR_COVER_CONFIG> GetView(HisEmrCoverConfigViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEmrCoverConfigDAO.GetView(filter.Query(), param);
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
