using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisDebateInviteUser
{
    public partial class HisDebateInviteUserDAO : EntityBase
    {
        public List<V_HIS_DEBATE_INVITE_USER> GetView(HisDebateInviteUserSO search, CommonParam param)
        {
            List<V_HIS_DEBATE_INVITE_USER> result = new List<V_HIS_DEBATE_INVITE_USER>();
            try
            {
                result = GetWorker.GetView(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }
            return result;
        }

        public V_HIS_DEBATE_INVITE_USER GetViewById(long id, HisDebateInviteUserSO search)
        {
            V_HIS_DEBATE_INVITE_USER result = null;

            try
            {
                result = GetWorker.GetViewById(id, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }
    }
}
