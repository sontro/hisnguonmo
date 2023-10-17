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
    }
}
