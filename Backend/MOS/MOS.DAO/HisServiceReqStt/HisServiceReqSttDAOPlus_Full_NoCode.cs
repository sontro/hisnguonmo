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
    }
}
