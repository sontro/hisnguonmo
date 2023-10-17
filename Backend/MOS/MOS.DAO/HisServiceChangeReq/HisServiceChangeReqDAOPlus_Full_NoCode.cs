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
    }
}
