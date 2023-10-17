using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisExpMestStt
{
    public partial class HisExpMestSttDAO : EntityBase
    {
        public List<V_HIS_EXP_MEST_STT> GetView(HisExpMestSttSO search, CommonParam param)
        {
            List<V_HIS_EXP_MEST_STT> result = new List<V_HIS_EXP_MEST_STT>();

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

        public HIS_EXP_MEST_STT GetByCode(string code, HisExpMestSttSO search)
        {
            HIS_EXP_MEST_STT result = null;

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
        
        public V_HIS_EXP_MEST_STT GetViewById(long id, HisExpMestSttSO search)
        {
            V_HIS_EXP_MEST_STT result = null;

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

        public V_HIS_EXP_MEST_STT GetViewByCode(string code, HisExpMestSttSO search)
        {
            V_HIS_EXP_MEST_STT result = null;

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

        public Dictionary<string, HIS_EXP_MEST_STT> GetDicByCode(HisExpMestSttSO search, CommonParam param)
        {
            Dictionary<string, HIS_EXP_MEST_STT> result = new Dictionary<string, HIS_EXP_MEST_STT>();
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
