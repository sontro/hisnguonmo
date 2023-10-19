using ACS.DAO.StagingObject;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.DAO.AcsAuthenRequest
{
    public partial class AcsAuthenRequestDAO : EntityBase
    {
        public List<V_ACS_AUTHEN_REQUEST> GetView(AcsAuthenRequestSO search, CommonParam param)
        {
            List<V_ACS_AUTHEN_REQUEST> result = new List<V_ACS_AUTHEN_REQUEST>();
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

        public V_ACS_AUTHEN_REQUEST GetViewById(long id, AcsAuthenRequestSO search)
        {
            V_ACS_AUTHEN_REQUEST result = null;

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
