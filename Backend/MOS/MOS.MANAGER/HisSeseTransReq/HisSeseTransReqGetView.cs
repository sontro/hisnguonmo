using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSeseTransReq
{
    partial class HisSeseTransReqGet : BusinessBase
    {
        internal List<V_HIS_SESE_TRANS_REQ> GetView(HisSeseTransReqViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSeseTransReqDAO.GetView(filter.Query(), param);
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
