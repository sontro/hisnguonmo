using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisDebateUser
{
    public partial class HisDebateUserDAO : EntityBase
    {
        public List<V_HIS_DEBATE_USER> GetView(HisDebateUserSO search, CommonParam param)
        {
            List<V_HIS_DEBATE_USER> result = new List<V_HIS_DEBATE_USER>();
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

        public V_HIS_DEBATE_USER GetViewById(long id, HisDebateUserSO search)
        {
            V_HIS_DEBATE_USER result = null;

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
