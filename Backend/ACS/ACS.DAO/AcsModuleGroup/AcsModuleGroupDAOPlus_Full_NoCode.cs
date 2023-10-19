using ACS.DAO.StagingObject;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.DAO.AcsModuleGroup
{
    public partial class AcsModuleGroupDAO : EntityBase
    {
        public List<V_ACS_MODULE_GROUP> GetView(AcsModuleGroupSO search, CommonParam param)
        {
            List<V_ACS_MODULE_GROUP> result = new List<V_ACS_MODULE_GROUP>();
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

        public V_ACS_MODULE_GROUP GetViewById(long id, AcsModuleGroupSO search)
        {
            V_ACS_MODULE_GROUP result = null;

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
