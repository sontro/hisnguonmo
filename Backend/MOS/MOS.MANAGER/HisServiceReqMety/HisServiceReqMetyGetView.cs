using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceReqMety
{
    partial class HisServiceReqMetyGet : BusinessBase
    {
        internal List<V_HIS_SERVICE_REQ_METY> GetView(HisServiceReqMetyViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceReqMetyDAO.GetView(filter.Query(), param);
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
