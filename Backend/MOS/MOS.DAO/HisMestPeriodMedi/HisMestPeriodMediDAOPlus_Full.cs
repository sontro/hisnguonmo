using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMestPeriodMedi
{
    public partial class HisMestPeriodMediDAO : EntityBase
    {
        public List<V_HIS_MEST_PERIOD_MEDI> GetView(HisMestPeriodMediSO search, CommonParam param)
        {
            List<V_HIS_MEST_PERIOD_MEDI> result = new List<V_HIS_MEST_PERIOD_MEDI>();

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

        public HIS_MEST_PERIOD_MEDI GetByCode(string code, HisMestPeriodMediSO search)
        {
            HIS_MEST_PERIOD_MEDI result = null;

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
        
        public V_HIS_MEST_PERIOD_MEDI GetViewById(long id, HisMestPeriodMediSO search)
        {
            V_HIS_MEST_PERIOD_MEDI result = null;

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

        public V_HIS_MEST_PERIOD_MEDI GetViewByCode(string code, HisMestPeriodMediSO search)
        {
            V_HIS_MEST_PERIOD_MEDI result = null;

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

        public Dictionary<string, HIS_MEST_PERIOD_MEDI> GetDicByCode(HisMestPeriodMediSO search, CommonParam param)
        {
            Dictionary<string, HIS_MEST_PERIOD_MEDI> result = new Dictionary<string, HIS_MEST_PERIOD_MEDI>();
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
