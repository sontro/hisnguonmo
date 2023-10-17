using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisIcdService
{
    public partial class HisIcdServiceDAO : EntityBase
    {
        public List<V_HIS_ICD_SERVICE> GetView(HisIcdServiceSO search, CommonParam param)
        {
            List<V_HIS_ICD_SERVICE> result = new List<V_HIS_ICD_SERVICE>();
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

        public V_HIS_ICD_SERVICE GetViewById(long id, HisIcdServiceSO search)
        {
            V_HIS_ICD_SERVICE result = null;

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
