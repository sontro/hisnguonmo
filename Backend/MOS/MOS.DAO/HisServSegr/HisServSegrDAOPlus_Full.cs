using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisServSegr
{
    public partial class HisServSegrDAO : EntityBase
    {
        public List<V_HIS_SERV_SEGR> GetView(HisServSegrSO search, CommonParam param)
        {
            List<V_HIS_SERV_SEGR> result = new List<V_HIS_SERV_SEGR>();

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

        public HIS_SERV_SEGR GetByCode(string code, HisServSegrSO search)
        {
            HIS_SERV_SEGR result = null;

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
        
        public V_HIS_SERV_SEGR GetViewById(long id, HisServSegrSO search)
        {
            V_HIS_SERV_SEGR result = null;

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

        public V_HIS_SERV_SEGR GetViewByCode(string code, HisServSegrSO search)
        {
            V_HIS_SERV_SEGR result = null;

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

        public Dictionary<string, HIS_SERV_SEGR> GetDicByCode(HisServSegrSO search, CommonParam param)
        {
            Dictionary<string, HIS_SERV_SEGR> result = new Dictionary<string, HIS_SERV_SEGR>();
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
