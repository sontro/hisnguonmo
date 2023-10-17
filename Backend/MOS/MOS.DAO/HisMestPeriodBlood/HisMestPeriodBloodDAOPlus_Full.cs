using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMestPeriodBlood
{
    public partial class HisMestPeriodBloodDAO : EntityBase
    {
        public List<V_HIS_MEST_PERIOD_BLOOD> GetView(HisMestPeriodBloodSO search, CommonParam param)
        {
            List<V_HIS_MEST_PERIOD_BLOOD> result = new List<V_HIS_MEST_PERIOD_BLOOD>();

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

        public HIS_MEST_PERIOD_BLOOD GetByCode(string code, HisMestPeriodBloodSO search)
        {
            HIS_MEST_PERIOD_BLOOD result = null;

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
        
        public V_HIS_MEST_PERIOD_BLOOD GetViewById(long id, HisMestPeriodBloodSO search)
        {
            V_HIS_MEST_PERIOD_BLOOD result = null;

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

        public V_HIS_MEST_PERIOD_BLOOD GetViewByCode(string code, HisMestPeriodBloodSO search)
        {
            V_HIS_MEST_PERIOD_BLOOD result = null;

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

        public Dictionary<string, HIS_MEST_PERIOD_BLOOD> GetDicByCode(HisMestPeriodBloodSO search, CommonParam param)
        {
            Dictionary<string, HIS_MEST_PERIOD_BLOOD> result = new Dictionary<string, HIS_MEST_PERIOD_BLOOD>();
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
