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

        public HIS_ICD_CM GetByCode(string code, HisIcdCmSO search)
        {
            HIS_ICD_CM result = null;

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

        public V_HIS_ICD_CM GetViewByCode(string code, HisIcdCmSO search)
        {
            V_HIS_ICD_CM result = null;

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

        public Dictionary<string, HIS_ICD_CM> GetDicByCode(HisIcdCmSO search, CommonParam param)
        {
            Dictionary<string, HIS_ICD_CM> result = new Dictionary<string, HIS_ICD_CM>();
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
