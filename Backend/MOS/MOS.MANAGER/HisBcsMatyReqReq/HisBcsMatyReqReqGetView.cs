using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBcsMatyReqReq
{
    partial class HisBcsMatyReqReqGet : BusinessBase
    {
        internal List<V_HIS_BCS_MATY_REQ_REQ> GetView(HisBcsMatyReqReqViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBcsMatyReqReqDAO.GetView(filter.Query(), param);
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
