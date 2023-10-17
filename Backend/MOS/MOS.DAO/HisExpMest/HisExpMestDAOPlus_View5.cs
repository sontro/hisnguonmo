using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisExpMest
{
    public partial class HisExpMestDAO : EntityBase
    {
        public List<V_HIS_EXP_MEST_5> GetView5(HisExpMestSO search, CommonParam param)
        {
            List<V_HIS_EXP_MEST_5> result = new List<V_HIS_EXP_MEST_5>();

            try
            {
                result = GetWorker.GetView5(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }

            return result;
        }

        public V_HIS_EXP_MEST_5 GetView5ById(long id, HisExpMestSO search)
        {
            V_HIS_EXP_MEST_5 result = null;

            try
            {
                result = GetWorker.GetView5ById(id, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }

        public V_HIS_EXP_MEST_5 GetView5ByCode(string code, HisExpMestSO search)
        {
            V_HIS_EXP_MEST_5 result = null;

            try
            {
                result = GetWorker.GetView5ByCode(code, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
