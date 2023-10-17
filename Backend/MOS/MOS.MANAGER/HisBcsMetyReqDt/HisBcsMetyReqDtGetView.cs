using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBcsMetyReqDt
{
    partial class HisBcsMetyReqDtGet : BusinessBase
    {
        internal List<V_HIS_BCS_METY_REQ_DT> GetView(HisBcsMetyReqDtViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBcsMetyReqDtDAO.GetView(filter.Query(), param);
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
