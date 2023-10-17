using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBcsMatyReqDt
{
    partial class HisBcsMatyReqDtGet : BusinessBase
    {
        internal List<V_HIS_BCS_MATY_REQ_DT> GetView(HisBcsMatyReqDtViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBcsMatyReqDtDAO.GetView(filter.Query(), param);
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
