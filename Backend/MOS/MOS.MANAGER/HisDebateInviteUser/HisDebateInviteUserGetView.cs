using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDebateInviteUser
{
    partial class HisDebateInviteUserGet : BusinessBase
    {
        internal List<V_HIS_DEBATE_INVITE_USER> GetView(HisDebateInviteUserViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDebateInviteUserDAO.GetView(filter.Query(), param);
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
