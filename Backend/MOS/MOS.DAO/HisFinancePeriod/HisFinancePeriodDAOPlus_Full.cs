using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisFinancePeriod
{
    public partial class HisFinancePeriodDAO : EntityBase
    {
        public List<V_HIS_FINANCE_PERIOD> GetView(HisFinancePeriodSO search, CommonParam param)
        {
            List<V_HIS_FINANCE_PERIOD> result = new List<V_HIS_FINANCE_PERIOD>();

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

        public HIS_FINANCE_PERIOD GetByCode(string code, HisFinancePeriodSO search)
        {
            HIS_FINANCE_PERIOD result = null;

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
        
        public V_HIS_FINANCE_PERIOD GetViewById(long id, HisFinancePeriodSO search)
        {
            V_HIS_FINANCE_PERIOD result = null;

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

        public V_HIS_FINANCE_PERIOD GetViewByCode(string code, HisFinancePeriodSO search)
        {
            V_HIS_FINANCE_PERIOD result = null;

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

        public Dictionary<string, HIS_FINANCE_PERIOD> GetDicByCode(HisFinancePeriodSO search, CommonParam param)
        {
            Dictionary<string, HIS_FINANCE_PERIOD> result = new Dictionary<string, HIS_FINANCE_PERIOD>();
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
