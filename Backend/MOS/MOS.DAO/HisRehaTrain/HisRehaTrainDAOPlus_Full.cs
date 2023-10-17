using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisRehaTrain
{
    public partial class HisRehaTrainDAO : EntityBase
    {
        public List<V_HIS_REHA_TRAIN> GetView(HisRehaTrainSO search, CommonParam param)
        {
            List<V_HIS_REHA_TRAIN> result = new List<V_HIS_REHA_TRAIN>();

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

        public HIS_REHA_TRAIN GetByCode(string code, HisRehaTrainSO search)
        {
            HIS_REHA_TRAIN result = null;

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
        
        public V_HIS_REHA_TRAIN GetViewById(long id, HisRehaTrainSO search)
        {
            V_HIS_REHA_TRAIN result = null;

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

        public V_HIS_REHA_TRAIN GetViewByCode(string code, HisRehaTrainSO search)
        {
            V_HIS_REHA_TRAIN result = null;

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

        public Dictionary<string, HIS_REHA_TRAIN> GetDicByCode(HisRehaTrainSO search, CommonParam param)
        {
            Dictionary<string, HIS_REHA_TRAIN> result = new Dictionary<string, HIS_REHA_TRAIN>();
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
