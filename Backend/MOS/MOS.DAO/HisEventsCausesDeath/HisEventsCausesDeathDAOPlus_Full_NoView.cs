using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisEventsCausesDeath
{
    public partial class HisEventsCausesDeathDAO : EntityBase
    {
        public HIS_EVENTS_CAUSES_DEATH GetByCode(string code, HisEventsCausesDeathSO search)
        {
            HIS_EVENTS_CAUSES_DEATH result = null;

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

        public Dictionary<string, HIS_EVENTS_CAUSES_DEATH> GetDicByCode(HisEventsCausesDeathSO search, CommonParam param)
        {
            Dictionary<string, HIS_EVENTS_CAUSES_DEATH> result = new Dictionary<string, HIS_EVENTS_CAUSES_DEATH>();
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
