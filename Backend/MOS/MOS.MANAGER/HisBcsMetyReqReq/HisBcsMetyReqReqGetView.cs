using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBcsMetyReqReq
{
    partial class HisBcsMetyReqReqGet : BusinessBase
    {
        internal List<V_HIS_BCS_METY_REQ_REQ> GetView(HisBcsMetyReqReqViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBcsMetyReqReqDAO.GetView(filter.Query(), param);
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
