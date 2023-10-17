using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisIcd
{
    public partial class HisIcdDAO : EntityBase
    {
        public List<V_HIS_ICD> GetView(HisIcdSO search, CommonParam param)
        {
            List<V_HIS_ICD> result = new List<V_HIS_ICD>();
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

        public V_HIS_ICD GetViewById(long id, HisIcdSO search)
        {
            V_HIS_ICD result = null;

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
