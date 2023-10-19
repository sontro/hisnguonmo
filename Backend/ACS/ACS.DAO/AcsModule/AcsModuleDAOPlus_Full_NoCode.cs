using ACS.DAO.StagingObject;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.DAO.AcsModule
{
    public partial class AcsModuleDAO : EntityBase
    {
        public List<V_ACS_MODULE> GetView(AcsModuleSO search, CommonParam param)
        {
            List<V_ACS_MODULE> result = new List<V_ACS_MODULE>();
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

        public V_ACS_MODULE GetViewById(long id, AcsModuleSO search)
        {
            V_ACS_MODULE result = null;

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
