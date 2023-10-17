using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceReq
{
    public partial class HisServiceReqDAO : EntityBase
    {
        public List<V_HIS_SERVICE_REQ_9> GetView9(HisServiceReqSO search, CommonParam param)
        {
            List<V_HIS_SERVICE_REQ_9> result = new List<V_HIS_SERVICE_REQ_9>();

            try
            {
                result = GetWorker.GetView9(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }

            return result;
        }
        
        public V_HIS_SERVICE_REQ_9 GetView9ById(long id, HisServiceReqSO search)
        {
            V_HIS_SERVICE_REQ_9 result = null;

            try
            {
                result = GetWorker.GetView9ById(id, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }

        public V_HIS_SERVICE_REQ_9 GetView9ByCode(string code, HisServiceReqSO search)
        {
            V_HIS_SERVICE_REQ_9 result = null;

            try
            {
                result = GetWorker.GetView9ByCode(code, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
