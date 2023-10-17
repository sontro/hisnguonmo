using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisPtttMethod
{
    public partial class HisPtttMethodDAO : EntityBase
    {
        public List<V_HIS_PTTT_METHOD> GetView(HisPtttMethodSO search, CommonParam param)
        {
            List<V_HIS_PTTT_METHOD> result = new List<V_HIS_PTTT_METHOD>();
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

        public V_HIS_PTTT_METHOD GetViewById(long id, HisPtttMethodSO search)
        {
            V_HIS_PTTT_METHOD result = null;

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
