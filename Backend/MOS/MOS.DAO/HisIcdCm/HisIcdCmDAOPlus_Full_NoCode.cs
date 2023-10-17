using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisIcdCm
{
    public partial class HisIcdCmDAO : EntityBase
    {
        public List<V_HIS_ICD_CM> GetView(HisIcdCmSO search, CommonParam param)
        {
            List<V_HIS_ICD_CM> result = new List<V_HIS_ICD_CM>();
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

        public V_HIS_ICD_CM GetViewById(long id, HisIcdCmSO search)
        {
            V_HIS_ICD_CM result = null;

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
