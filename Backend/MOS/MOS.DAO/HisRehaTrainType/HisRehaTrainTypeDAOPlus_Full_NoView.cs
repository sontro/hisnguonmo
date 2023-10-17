using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisRehaTrainType
{
    public partial class HisRehaTrainTypeDAO : EntityBase
    {
        public HIS_REHA_TRAIN_TYPE GetByCode(string code, HisRehaTrainTypeSO search)
        {
            HIS_REHA_TRAIN_TYPE result = null;

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

        public Dictionary<string, HIS_REHA_TRAIN_TYPE> GetDicByCode(HisRehaTrainTypeSO search, CommonParam param)
        {
            Dictionary<string, HIS_REHA_TRAIN_TYPE> result = new Dictionary<string, HIS_REHA_TRAIN_TYPE>();
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
