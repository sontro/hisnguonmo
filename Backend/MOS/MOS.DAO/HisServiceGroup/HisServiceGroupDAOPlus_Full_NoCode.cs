using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceGroup
{
    public partial class HisServiceGroupDAO : EntityBase
    {
        public List<V_HIS_SERVICE_GROUP> GetView(HisServiceGroupSO search, CommonParam param)
        {
            List<V_HIS_SERVICE_GROUP> result = new List<V_HIS_SERVICE_GROUP>();
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

        public V_HIS_SERVICE_GROUP GetViewById(long id, HisServiceGroupSO search)
        {
            V_HIS_SERVICE_GROUP result = null;

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
