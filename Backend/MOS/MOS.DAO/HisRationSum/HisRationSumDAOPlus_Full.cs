using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisRationSum
{
    public partial class HisRationSumDAO : EntityBase
    {
        public List<V_HIS_RATION_SUM> GetView(HisRationSumSO search, CommonParam param)
        {
            List<V_HIS_RATION_SUM> result = new List<V_HIS_RATION_SUM>();

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

        public HIS_RATION_SUM GetByCode(string code, HisRationSumSO search)
        {
            HIS_RATION_SUM result = null;

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
        
        public V_HIS_RATION_SUM GetViewById(long id, HisRationSumSO search)
        {
            V_HIS_RATION_SUM result = null;

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

        public V_HIS_RATION_SUM GetViewByCode(string code, HisRationSumSO search)
        {
            V_HIS_RATION_SUM result = null;

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

        public Dictionary<string, HIS_RATION_SUM> GetDicByCode(HisRationSumSO search, CommonParam param)
        {
            Dictionary<string, HIS_RATION_SUM> result = new Dictionary<string, HIS_RATION_SUM>();
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
