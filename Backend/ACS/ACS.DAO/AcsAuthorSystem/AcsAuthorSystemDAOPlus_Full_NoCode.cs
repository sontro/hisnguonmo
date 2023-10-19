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
    }
}
