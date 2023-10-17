using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisEmotionlessMethod
{
    public partial class HisEmotionlessMethodDAO : EntityBase
    {
        public List<V_HIS_EMOTIONLESS_METHOD> GetView(HisEmotionlessMethodSO search, CommonParam param)
        {
            List<V_HIS_EMOTIONLESS_METHOD> result = new List<V_HIS_EMOTIONLESS_METHOD>();
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

        public V_HIS_EMOTIONLESS_METHOD GetViewById(long id, HisEmotionlessMethodSO search)
        {
            V_HIS_EMOTIONLESS_METHOD result = null;

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
