using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisRejectAlert
{
    public partial class HisRejectAlertDAO : EntityBase
    {
        public List<V_HIS_REJECT_ALERT> GetView(HisRejectAlertSO search, CommonParam param)
        {
            List<V_HIS_REJECT_ALERT> result = new List<V_HIS_REJECT_ALERT>();

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

        public HIS_REJECT_ALERT GetByCode(string code, HisRejectAlertSO search)
        {
            HIS_REJECT_ALERT result = null;

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
        
        public V_HIS_REJECT_ALERT GetViewById(long id, HisRejectAlertSO search)
        {
            V_HIS_REJECT_ALERT result = null;

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

        public V_HIS_REJECT_ALERT GetViewByCode(string code, HisRejectAlertSO search)
        {
            V_HIS_REJECT_ALERT result = null;

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

        public Dictionary<string, HIS_REJECT_ALERT> GetDicByCode(HisRejectAlertSO search, CommonParam param)
        {
            Dictionary<string, HIS_REJECT_ALERT> result = new Dictionary<string, HIS_REJECT_ALERT>();
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
