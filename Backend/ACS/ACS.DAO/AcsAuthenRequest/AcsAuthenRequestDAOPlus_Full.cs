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

        public ACS_AUTHEN_REQUEST GetByCode(string code, AcsAuthenRequestSO search)
        {
            ACS_AUTHEN_REQUEST result = null;

            try
            {
                result = GetWorker.GetByCode(code, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
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

        public V_ACS_AUTHEN_REQUEST GetViewByCode(string code, AcsAuthenRequestSO search)
        {
            V_ACS_AUTHEN_REQUEST result = null;

            try
            {
                result = GetWorker.GetViewByCode(code, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        public Dictionary<string, ACS_AUTHEN_REQUEST> GetDicByCode(AcsAuthenRequestSO search, CommonParam param)
        {
            Dictionary<string, ACS_AUTHEN_REQUEST> result = new Dictionary<string, ACS_AUTHEN_REQUEST>();
            try
            {
                result = GetWorker.GetDicByCode(search, param);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }

            return result;
        }

        public bool ExistsCode(string code, long? id)
        {
            try
            {
                return CheckWorker.ExistsCode(code, id);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                throw;
            }
        }
    }
}
