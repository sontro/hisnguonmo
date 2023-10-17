using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisIcdGroup
{
    public partial class HisIcdGroupDAO : EntityBase
    {
        public List<V_HIS_ICD_GROUP> GetView(HisIcdGroupSO search, CommonParam param)
        {
            List<V_HIS_ICD_GROUP> result = new List<V_HIS_ICD_GROUP>();
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

        public V_HIS_ICD_GROUP GetViewById(long id, HisIcdGroupSO search)
        {
            V_HIS_ICD_GROUP result = null;

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
