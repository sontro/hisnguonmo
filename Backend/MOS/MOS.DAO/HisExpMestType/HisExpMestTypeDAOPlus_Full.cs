using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisExpMestType
{
    public partial class HisExpMestTypeDAO : EntityBase
    {
        public List<V_HIS_EXP_MEST_TYPE> GetView(HisExpMestTypeSO search, CommonParam param)
        {
            List<V_HIS_EXP_MEST_TYPE> result = new List<V_HIS_EXP_MEST_TYPE>();

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

        public HIS_EXP_MEST_TYPE GetByCode(string code, HisExpMestTypeSO search)
        {
            HIS_EXP_MEST_TYPE result = null;

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
        
        public V_HIS_EXP_MEST_TYPE GetViewById(long id, HisExpMestTypeSO search)
        {
            V_HIS_EXP_MEST_TYPE result = null;

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

        public V_HIS_EXP_MEST_TYPE GetViewByCode(string code, HisExpMestTypeSO search)
        {
            V_HIS_EXP_MEST_TYPE result = null;

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

        public Dictionary<string, HIS_EXP_MEST_TYPE> GetDicByCode(HisExpMestTypeSO search, CommonParam param)
        {
            Dictionary<string, HIS_EXP_MEST_TYPE> result = new Dictionary<string, HIS_EXP_MEST_TYPE>();
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
