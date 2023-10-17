using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisQcNormation
{
    public partial class HisQcNormationDAO : EntityBase
    {
        public List<V_HIS_QC_NORMATION> GetView(HisQcNormationSO search, CommonParam param)
        {
            List<V_HIS_QC_NORMATION> result = new List<V_HIS_QC_NORMATION>();
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

        public V_HIS_QC_NORMATION GetViewById(long id, HisQcNormationSO search)
        {
            V_HIS_QC_NORMATION result = null;

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
