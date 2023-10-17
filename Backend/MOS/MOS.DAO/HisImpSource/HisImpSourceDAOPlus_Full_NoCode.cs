using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisImpSource
{
    public partial class HisImpSourceDAO : EntityBase
    {
        public List<V_HIS_IMP_SOURCE> GetView(HisImpSourceSO search, CommonParam param)
        {
            List<V_HIS_IMP_SOURCE> result = new List<V_HIS_IMP_SOURCE>();
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

        public V_HIS_IMP_SOURCE GetViewById(long id, HisImpSourceSO search)
        {
            V_HIS_IMP_SOURCE result = null;

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
