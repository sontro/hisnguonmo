using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTransReq
{
    partial class HisTransReqGet : BusinessBase
    {
        internal List<V_HIS_TRANS_REQ> GetView(HisTransReqViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTransReqDAO.GetView(filter.Query(), param);
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
