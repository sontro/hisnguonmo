using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceReqStt
{
    public partial class HisServiceReqSttDAO : EntityBase
    {
        public List<V_HIS_SERVICE_REQ_STT> GetView(HisServiceReqSttSO search, CommonParam param)
        {
            List<V_HIS_SERVICE_REQ_STT> result = new List<V_HIS_SERVICE_REQ_STT>();

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

        public HIS_SERVICE_REQ_STT GetByCode(string code, HisServiceReqSttSO search)
        {
            HIS_SERVICE_REQ_STT result = null;

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
        
        public V_HIS_SERVICE_REQ_STT GetViewById(long id, HisServiceReqSttSO search)
        {
            V_HIS_SERVICE_REQ_STT result = null;

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

        public V_HIS_SERVICE_REQ_STT GetViewByCode(string code, HisServiceReqSttSO search)
        {
            V_HIS_SERVICE_REQ_STT result = null;

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

        public Dictionary<string, HIS_SERVICE_REQ_STT> GetDicByCode(HisServiceReqSttSO search, CommonParam param)
        {
            Dictionary<string, HIS_SERVICE_REQ_STT> result = new Dictionary<string, HIS_SERVICE_REQ_STT>();
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
