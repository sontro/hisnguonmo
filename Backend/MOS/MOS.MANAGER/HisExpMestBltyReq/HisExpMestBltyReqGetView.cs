using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestBltyReq
{
    partial class HisExpMestBltyReqGet : BusinessBase
    {
        internal List<V_HIS_EXP_MEST_BLTY_REQ> GetView(HisExpMestBltyReqViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExpMestBltyReqDAO.GetView(filter.Query(), param);
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
