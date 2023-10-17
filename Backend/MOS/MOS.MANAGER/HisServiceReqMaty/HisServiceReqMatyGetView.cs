using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceReqMaty
{
    partial class HisServiceReqMatyGet : BusinessBase
    {
        internal List<V_HIS_SERVICE_REQ_MATY> GetView(HisServiceReqMatyViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceReqMatyDAO.GetView(filter.Query(), param);
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
