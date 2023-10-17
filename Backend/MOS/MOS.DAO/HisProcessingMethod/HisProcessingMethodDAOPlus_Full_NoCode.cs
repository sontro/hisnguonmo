using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisProcessingMethod
{
    public partial class HisProcessingMethodDAO : EntityBase
    {
        public List<V_HIS_PROCESSING_METHOD> GetView(HisProcessingMethodSO search, CommonParam param)
        {
            List<V_HIS_PROCESSING_METHOD> result = new List<V_HIS_PROCESSING_METHOD>();
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

        public V_HIS_PROCESSING_METHOD GetViewById(long id, HisProcessingMethodSO search)
        {
            V_HIS_PROCESSING_METHOD result = null;

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
