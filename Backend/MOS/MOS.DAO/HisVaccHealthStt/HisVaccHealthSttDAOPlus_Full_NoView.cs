using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisVaccHealthStt
{
    public partial class HisVaccHealthSttDAO : EntityBase
    {
        public HIS_VACC_HEALTH_STT GetByCode(string code, HisVaccHealthSttSO search)
        {
            HIS_VACC_HEALTH_STT result = null;

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

        public Dictionary<string, HIS_VACC_HEALTH_STT> GetDicByCode(HisVaccHealthSttSO search, CommonParam param)
        {
            Dictionary<string, HIS_VACC_HEALTH_STT> result = new Dictionary<string, HIS_VACC_HEALTH_STT>();
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
