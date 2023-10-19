using ACS.DAO.StagingObject;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.DAO.AcsAuthorSystem
{
    public partial class AcsAuthorSystemDAO : EntityBase
    {
        public List<V_ACS_AUTHOR_SYSTEM> GetView(AcsAuthorSystemSO search, CommonParam param)
        {
            List<V_ACS_AUTHOR_SYSTEM> result = new List<V_ACS_AUTHOR_SYSTEM>();

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

        public ACS_AUTHOR_SYSTEM GetByCode(string code, AcsAuthorSystemSO search)
        {
            ACS_AUTHOR_SYSTEM result = null;

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
        
        public V_ACS_AUTHOR_SYSTEM GetViewById(long id, AcsAuthorSystemSO search)
        {
            V_ACS_AUTHOR_SYSTEM result = null;

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

        public V_ACS_AUTHOR_SYSTEM GetViewByCode(string code, AcsAuthorSystemSO search)
        {
            V_ACS_AUTHOR_SYSTEM result = null;

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

        public Dictionary<string, ACS_AUTHOR_SYSTEM> GetDicByCode(AcsAuthorSystemSO search, CommonParam param)
        {
            Dictionary<string, ACS_AUTHOR_SYSTEM> result = new Dictionary<string, ACS_AUTHOR_SYSTEM>();
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
