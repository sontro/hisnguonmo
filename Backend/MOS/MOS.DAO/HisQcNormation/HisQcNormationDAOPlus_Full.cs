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

        public HIS_QC_NORMATION GetByCode(string code, HisQcNormationSO search)
        {
            HIS_QC_NORMATION result = null;

            try
            {
                result = GetWorker.GetByCode(code, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
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

        public V_HIS_QC_NORMATION GetViewByCode(string code, HisQcNormationSO search)
        {
            V_HIS_QC_NORMATION result = null;

            try
            {
                result = GetWorker.GetViewByCode(code, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        public Dictionary<string, HIS_QC_NORMATION> GetDicByCode(HisQcNormationSO search, CommonParam param)
        {
            Dictionary<string, HIS_QC_NORMATION> result = new Dictionary<string, HIS_QC_NORMATION>();
            try
            {
                result = GetWorker.GetDicByCode(search, param);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }

            return result;
        }

        public bool ExistsCode(string code, long? id)
        {
            try
            {
                return CheckWorker.ExistsCode(code, id);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                throw;
            }
        }
    }
}
