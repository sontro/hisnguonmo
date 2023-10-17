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

        public HIS_EMOTIONLESS_METHOD GetByCode(string code, HisEmotionlessMethodSO search)
        {
            HIS_EMOTIONLESS_METHOD result = null;

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

        public V_HIS_EMOTIONLESS_METHOD GetViewByCode(string code, HisEmotionlessMethodSO search)
        {
            V_HIS_EMOTIONLESS_METHOD result = null;

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

        public Dictionary<string, HIS_EMOTIONLESS_METHOD> GetDicByCode(HisEmotionlessMethodSO search, CommonParam param)
        {
            Dictionary<string, HIS_EMOTIONLESS_METHOD> result = new Dictionary<string, HIS_EMOTIONLESS_METHOD>();
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
