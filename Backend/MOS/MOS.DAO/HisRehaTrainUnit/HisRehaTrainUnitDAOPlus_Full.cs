using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisRehaTrainUnit
{
    public partial class HisRehaTrainUnitDAO : EntityBase
    {
        public List<V_HIS_REHA_TRAIN_UNIT> GetView(HisRehaTrainUnitSO search, CommonParam param)
        {
            List<V_HIS_REHA_TRAIN_UNIT> result = new List<V_HIS_REHA_TRAIN_UNIT>();

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

        public HIS_REHA_TRAIN_UNIT GetByCode(string code, HisRehaTrainUnitSO search)
        {
            HIS_REHA_TRAIN_UNIT result = null;

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
        
        public V_HIS_REHA_TRAIN_UNIT GetViewById(long id, HisRehaTrainUnitSO search)
        {
            V_HIS_REHA_TRAIN_UNIT result = null;

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

        public V_HIS_REHA_TRAIN_UNIT GetViewByCode(string code, HisRehaTrainUnitSO search)
        {
            V_HIS_REHA_TRAIN_UNIT result = null;

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

        public Dictionary<string, HIS_REHA_TRAIN_UNIT> GetDicByCode(HisRehaTrainUnitSO search, CommonParam param)
        {
            Dictionary<string, HIS_REHA_TRAIN_UNIT> result = new Dictionary<string, HIS_REHA_TRAIN_UNIT>();
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
