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

        public HIS_IMP_SOURCE GetByCode(string code, HisImpSourceSO search)
        {
            HIS_IMP_SOURCE result = null;

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

        public V_HIS_IMP_SOURCE GetViewByCode(string code, HisImpSourceSO search)
        {
            V_HIS_IMP_SOURCE result = null;

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

        public Dictionary<string, HIS_IMP_SOURCE> GetDicByCode(HisImpSourceSO search, CommonParam param)
        {
            Dictionary<string, HIS_IMP_SOURCE> result = new Dictionary<string, HIS_IMP_SOURCE>();
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
