using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisRationSumStt
{
    public partial class HisRationSumSttDAO : EntityBase
    {
        public HIS_RATION_SUM_STT GetByCode(string code, HisRationSumSttSO search)
        {
            HIS_RATION_SUM_STT result = null;

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

        public Dictionary<string, HIS_RATION_SUM_STT> GetDicByCode(HisRationSumSttSO search, CommonParam param)
        {
            Dictionary<string, HIS_RATION_SUM_STT> result = new Dictionary<string, HIS_RATION_SUM_STT>();
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
