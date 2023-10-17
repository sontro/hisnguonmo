using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisDeathCause
{
    public partial class HisDeathCauseDAO : EntityBase
    {
        public List<V_HIS_DEATH_CAUSE> GetView(HisDeathCauseSO search, CommonParam param)
        {
            List<V_HIS_DEATH_CAUSE> result = new List<V_HIS_DEATH_CAUSE>();

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

        public HIS_DEATH_CAUSE GetByCode(string code, HisDeathCauseSO search)
        {
            HIS_DEATH_CAUSE result = null;

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
        
        public V_HIS_DEATH_CAUSE GetViewById(long id, HisDeathCauseSO search)
        {
            V_HIS_DEATH_CAUSE result = null;

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

        public V_HIS_DEATH_CAUSE GetViewByCode(string code, HisDeathCauseSO search)
        {
            V_HIS_DEATH_CAUSE result = null;

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

        public Dictionary<string, HIS_DEATH_CAUSE> GetDicByCode(HisDeathCauseSO search, CommonParam param)
        {
            Dictionary<string, HIS_DEATH_CAUSE> result = new Dictionary<string, HIS_DEATH_CAUSE>();
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
