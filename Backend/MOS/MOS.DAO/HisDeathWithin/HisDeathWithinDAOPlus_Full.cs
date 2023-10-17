using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisDeathWithin
{
    public partial class HisDeathWithinDAO : EntityBase
    {
        public List<V_HIS_DEATH_WITHIN> GetView(HisDeathWithinSO search, CommonParam param)
        {
            List<V_HIS_DEATH_WITHIN> result = new List<V_HIS_DEATH_WITHIN>();

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

        public HIS_DEATH_WITHIN GetByCode(string code, HisDeathWithinSO search)
        {
            HIS_DEATH_WITHIN result = null;

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
        
        public V_HIS_DEATH_WITHIN GetViewById(long id, HisDeathWithinSO search)
        {
            V_HIS_DEATH_WITHIN result = null;

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

        public V_HIS_DEATH_WITHIN GetViewByCode(string code, HisDeathWithinSO search)
        {
            V_HIS_DEATH_WITHIN result = null;

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

        public Dictionary<string, HIS_DEATH_WITHIN> GetDicByCode(HisDeathWithinSO search, CommonParam param)
        {
            Dictionary<string, HIS_DEATH_WITHIN> result = new Dictionary<string, HIS_DEATH_WITHIN>();
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
