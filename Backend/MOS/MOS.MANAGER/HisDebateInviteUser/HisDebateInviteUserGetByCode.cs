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
        internal HIS_DEBATE_INVITE_USER GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisDebateInviteUserFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DEBATE_INVITE_USER GetByCode(string code, HisDebateInviteUserFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDebateInviteUserDAO.GetByCode(code, filter.Query());
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
