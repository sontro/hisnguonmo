using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisNumOrderBlock
{
    public partial class HisNumOrderBlockDAO : EntityBase
    {
        public List<V_HIS_NUM_ORDER_BLOCK> GetView(HisNumOrderBlockSO search, CommonParam param)
        {
            List<V_HIS_NUM_ORDER_BLOCK> result = new List<V_HIS_NUM_ORDER_BLOCK>();

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

        public HIS_NUM_ORDER_BLOCK GetByCode(string code, HisNumOrderBlockSO search)
        {
            HIS_NUM_ORDER_BLOCK result = null;

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
        
        public V_HIS_NUM_ORDER_BLOCK GetViewById(long id, HisNumOrderBlockSO search)
        {
            V_HIS_NUM_ORDER_BLOCK result = null;

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

        public V_HIS_NUM_ORDER_BLOCK GetViewByCode(string code, HisNumOrderBlockSO search)
        {
            V_HIS_NUM_ORDER_BLOCK result = null;

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

        public Dictionary<string, HIS_NUM_ORDER_BLOCK> GetDicByCode(HisNumOrderBlockSO search, CommonParam param)
        {
            Dictionary<string, HIS_NUM_ORDER_BLOCK> result = new Dictionary<string, HIS_NUM_ORDER_BLOCK>();
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
