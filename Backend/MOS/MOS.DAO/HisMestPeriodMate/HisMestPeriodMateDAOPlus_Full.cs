using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMestPeriodMate
{
    public partial class HisMestPeriodMateDAO : EntityBase
    {
        public List<V_HIS_MEST_PERIOD_MATE> GetView(HisMestPeriodMateSO search, CommonParam param)
        {
            List<V_HIS_MEST_PERIOD_MATE> result = new List<V_HIS_MEST_PERIOD_MATE>();

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

        public HIS_MEST_PERIOD_MATE GetByCode(string code, HisMestPeriodMateSO search)
        {
            HIS_MEST_PERIOD_MATE result = null;

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
        
        public V_HIS_MEST_PERIOD_MATE GetViewById(long id, HisMestPeriodMateSO search)
        {
            V_HIS_MEST_PERIOD_MATE result = null;

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

        public V_HIS_MEST_PERIOD_MATE GetViewByCode(string code, HisMestPeriodMateSO search)
        {
            V_HIS_MEST_PERIOD_MATE result = null;

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

        public Dictionary<string, HIS_MEST_PERIOD_MATE> GetDicByCode(HisMestPeriodMateSO search, CommonParam param)
        {
            Dictionary<string, HIS_MEST_PERIOD_MATE> result = new Dictionary<string, HIS_MEST_PERIOD_MATE>();
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
