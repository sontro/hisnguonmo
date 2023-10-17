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
        internal HisDebateInviteUserGet()
            : base()
        {

        }

        internal HisDebateInviteUserGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_DEBATE_INVITE_USER> Get(HisDebateInviteUserFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDebateInviteUserDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DEBATE_INVITE_USER GetById(long id)
        {
            try
            {
                return GetById(id, new HisDebateInviteUserFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DEBATE_INVITE_USER GetById(long id, HisDebateInviteUserFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDebateInviteUserDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_DEBATE_INVITE_USER> GetByDebateId(long id)
        {
            try
            {
                HisDebateInviteUserFilterQuery filter = new HisDebateInviteUserFilterQuery();
                filter.DEBATE_ID = id;
                return this.Get(filter);
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
