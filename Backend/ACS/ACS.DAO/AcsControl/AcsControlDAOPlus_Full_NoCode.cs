using ACS.DAO.StagingObject;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.DAO.AcsControl
{
    public partial class AcsControlDAO : EntityBase
    {
        public List<V_ACS_CONTROL> GetView(AcsControlSO search, CommonParam param)
        {
            List<V_ACS_CONTROL> result = new List<V_ACS_CONTROL>();
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

        public V_ACS_CONTROL GetViewById(long id, AcsControlSO search)
        {
            V_ACS_CONTROL result = null;

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
