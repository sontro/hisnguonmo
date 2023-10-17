using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisImpMestStt
{
    public partial class HisImpMestSttDAO : EntityBase
    {
        public List<V_HIS_IMP_MEST_STT> GetView(HisImpMestSttSO search, CommonParam param)
        {
            List<V_HIS_IMP_MEST_STT> result = new List<V_HIS_IMP_MEST_STT>();
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

        public V_HIS_IMP_MEST_STT GetViewById(long id, HisImpMestSttSO search)
        {
            V_HIS_IMP_MEST_STT result = null;

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
