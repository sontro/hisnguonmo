using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDebateEkipUser
{
    partial class HisDebateEkipUserGet : BusinessBase
    {
        internal List<V_HIS_DEBATE_EKIP_USER> GetView(HisDebateEkipUserViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDebateEkipUserDAO.GetView(filter.Query(), param);
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
