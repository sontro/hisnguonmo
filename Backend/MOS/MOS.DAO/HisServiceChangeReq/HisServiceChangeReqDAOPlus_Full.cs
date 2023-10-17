using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceChangeReq
{
    public partial class HisServiceChangeReqDAO : EntityBase
    {
        public List<V_HIS_SERVICE_CHANGE_REQ> GetView(HisServiceChangeReqSO search, CommonParam param)
        {
            List<V_HIS_SERVICE_CHANGE_REQ> result = new List<V_HIS_SERVICE_CHANGE_REQ>();

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

        public HIS_SERVICE_CHANGE_REQ GetByCode(string code, HisServiceChangeReqSO search)
        {
            HIS_SERVICE_CHANGE_REQ result = null;

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
        
        public V_HIS_SERVICE_CHANGE_REQ GetViewById(long id, HisServiceChangeReqSO search)
        {
            V_HIS_SERVICE_CHANGE_REQ result = null;

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

        public V_HIS_SERVICE_CHANGE_REQ GetViewByCode(string code, HisServiceChangeReqSO search)
        {
            V_HIS_SERVICE_CHANGE_REQ result = null;

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

        public Dictionary<string, HIS_SERVICE_CHANGE_REQ> GetDicByCode(HisServiceChangeReqSO search, CommonParam param)
        {
            Dictionary<string, HIS_SERVICE_CHANGE_REQ> result = new Dictionary<string, HIS_SERVICE_CHANGE_REQ>();
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
