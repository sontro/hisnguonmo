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
        internal V_HIS_DEBATE_INVITE_USER GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisDebateInviteUserViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_DEBATE_INVITE_USER GetViewByCode(string code, HisDebateInviteUserViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDebateInviteUserDAO.GetViewByCode(code, filter.Query());
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
