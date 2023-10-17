using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEmrCoverType
{
    partial class HisEmrCoverTypeGet : BusinessBase
    {
        internal List<V_HIS_EMR_COVER_TYPE> GetView(HisEmrCoverTypeViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEmrCoverTypeDAO.GetView(filter.Query(), param);
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
