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

        public ACS_CONTROL GetByCode(string code, AcsControlSO search)
        {
            ACS_CONTROL result = null;

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

        public V_ACS_CONTROL GetViewByCode(string code, AcsControlSO search)
        {
            V_ACS_CONTROL result = null;

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

        public Dictionary<string, ACS_CONTROL> GetDicByCode(AcsControlSO search, CommonParam param)
        {
            Dictionary<string, ACS_CONTROL> result = new Dictionary<string, ACS_CONTROL>();
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

        public bool ExistsCode(string code, long? id, long moduleId)
        {
            try
            {
                return CheckWorker.ExistsCode(code, id, moduleId);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                throw;
            }
        }
    }
}
