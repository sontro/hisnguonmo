using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisInfusionSum
{
    public partial class HisInfusionSumDAO : EntityBase
    {
        public List<V_HIS_INFUSION_SUM> GetView(HisInfusionSumSO search, CommonParam param)
        {
            List<V_HIS_INFUSION_SUM> result = new List<V_HIS_INFUSION_SUM>();

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

        public HIS_INFUSION_SUM GetByCode(string code, HisInfusionSumSO search)
        {
            HIS_INFUSION_SUM result = null;

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
        
        public V_HIS_INFUSION_SUM GetViewById(long id, HisInfusionSumSO search)
        {
            V_HIS_INFUSION_SUM result = null;

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

        public V_HIS_INFUSION_SUM GetViewByCode(string code, HisInfusionSumSO search)
        {
            V_HIS_INFUSION_SUM result = null;

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

        public Dictionary<string, HIS_INFUSION_SUM> GetDicByCode(HisInfusionSumSO search, CommonParam param)
        {
            Dictionary<string, HIS_INFUSION_SUM> result = new Dictionary<string, HIS_INFUSION_SUM>();
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
