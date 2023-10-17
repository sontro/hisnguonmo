using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceReq
{
    public partial class HisServiceReqDAO : EntityBase
    {
        public List<V_HIS_SERVICE_REQ_8> GetView8(HisServiceReqSO search, CommonParam param)
        {
            List<V_HIS_SERVICE_REQ_8> result = new List<V_HIS_SERVICE_REQ_8>();

            try
            {
                result = GetWorker.GetView8(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }

            return result;
        }
        
        public V_HIS_SERVICE_REQ_8 GetView8ById(long id, HisServiceReqSO search)
        {
            V_HIS_SERVICE_REQ_8 result = null;

            try
            {
                result = GetWorker.GetView8ById(id, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }

        public V_HIS_SERVICE_REQ_8 GetView8ByCode(string code, HisServiceReqSO search)
        {
            V_HIS_SERVICE_REQ_8 result = null;

            try
            {
                result = GetWorker.GetView8ByCode(code, search);
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
