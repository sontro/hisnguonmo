using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisEmotionlessResult
{
    public partial class HisEmotionlessResultDAO : EntityBase
    {
        public List<V_HIS_EMOTIONLESS_RESULT> GetView(HisEmotionlessResultSO search, CommonParam param)
        {
            List<V_HIS_EMOTIONLESS_RESULT> result = new List<V_HIS_EMOTIONLESS_RESULT>();
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

        public V_HIS_EMOTIONLESS_RESULT GetViewById(long id, HisEmotionlessResultSO search)
        {
            V_HIS_EMOTIONLESS_RESULT result = null;

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
