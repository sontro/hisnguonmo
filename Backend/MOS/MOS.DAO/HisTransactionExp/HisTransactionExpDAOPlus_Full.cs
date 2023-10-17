using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisTransactionExp
{
    public partial class HisTransactionExpDAO : EntityBase
    {
        public List<V_HIS_TRANSACTION_EXP> GetView(HisTransactionExpSO search, CommonParam param)
        {
            List<V_HIS_TRANSACTION_EXP> result = new List<V_HIS_TRANSACTION_EXP>();

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

        public HIS_TRANSACTION_EXP GetByCode(string code, HisTransactionExpSO search)
        {
            HIS_TRANSACTION_EXP result = null;

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
        
        public V_HIS_TRANSACTION_EXP GetViewById(long id, HisTransactionExpSO search)
        {
            V_HIS_TRANSACTION_EXP result = null;

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

        public V_HIS_TRANSACTION_EXP GetViewByCode(string code, HisTransactionExpSO search)
        {
            V_HIS_TRANSACTION_EXP result = null;

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

        public Dictionary<string, HIS_TRANSACTION_EXP> GetDicByCode(HisTransactionExpSO search, CommonParam param)
        {
            Dictionary<string, HIS_TRANSACTION_EXP> result = new Dictionary<string, HIS_TRANSACTION_EXP>();
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
