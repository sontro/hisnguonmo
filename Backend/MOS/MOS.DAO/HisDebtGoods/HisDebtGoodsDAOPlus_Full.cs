using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisDebtGoods
{
    public partial class HisDebtGoodsDAO : EntityBase
    {
        public List<V_HIS_DEBT_GOODS> GetView(HisDebtGoodsSO search, CommonParam param)
        {
            List<V_HIS_DEBT_GOODS> result = new List<V_HIS_DEBT_GOODS>();

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

        public HIS_DEBT_GOODS GetByCode(string code, HisDebtGoodsSO search)
        {
            HIS_DEBT_GOODS result = null;

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
        
        public V_HIS_DEBT_GOODS GetViewById(long id, HisDebtGoodsSO search)
        {
            V_HIS_DEBT_GOODS result = null;

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

        public V_HIS_DEBT_GOODS GetViewByCode(string code, HisDebtGoodsSO search)
        {
            V_HIS_DEBT_GOODS result = null;

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

        public Dictionary<string, HIS_DEBT_GOODS> GetDicByCode(HisDebtGoodsSO search, CommonParam param)
        {
            Dictionary<string, HIS_DEBT_GOODS> result = new Dictionary<string, HIS_DEBT_GOODS>();
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
