using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisRehaSum
{
    public partial class HisRehaSumDAO : EntityBase
    {
        public List<V_HIS_REHA_SUM> GetView(HisRehaSumSO search, CommonParam param)
        {
            List<V_HIS_REHA_SUM> result = new List<V_HIS_REHA_SUM>();

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

        public HIS_REHA_SUM GetByCode(string code, HisRehaSumSO search)
        {
            HIS_REHA_SUM result = null;

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
        
        public V_HIS_REHA_SUM GetViewById(long id, HisRehaSumSO search)
        {
            V_HIS_REHA_SUM result = null;

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

        public V_HIS_REHA_SUM GetViewByCode(string code, HisRehaSumSO search)
        {
            V_HIS_REHA_SUM result = null;

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

        public Dictionary<string, HIS_REHA_SUM> GetDicByCode(HisRehaSumSO search, CommonParam param)
        {
            Dictionary<string, HIS_REHA_SUM> result = new Dictionary<string, HIS_REHA_SUM>();
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
