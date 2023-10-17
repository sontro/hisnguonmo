using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisEventsCausesDeath
{
    public partial class HisEventsCausesDeathDAO : EntityBase
    {
        public List<V_HIS_EVENTS_CAUSES_DEATH> GetView(HisEventsCausesDeathSO search, CommonParam param)
        {
            List<V_HIS_EVENTS_CAUSES_DEATH> result = new List<V_HIS_EVENTS_CAUSES_DEATH>();

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
        
        public V_HIS_EVENTS_CAUSES_DEATH GetViewById(long id, HisEventsCausesDeathSO search)
        {
            V_HIS_EVENTS_CAUSES_DEATH result = null;

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

        public V_HIS_EVENTS_CAUSES_DEATH GetViewByCode(string code, HisEventsCausesDeathSO search)
        {
            V_HIS_EVENTS_CAUSES_DEATH result = null;

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
