using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceReqMety
{
    public partial class HisServiceReqMetyDAO : EntityBase
    {
        public List<V_HIS_SERVICE_REQ_METY> GetView(HisServiceReqMetySO search, CommonParam param)
        {
            List<V_HIS_SERVICE_REQ_METY> result = new List<V_HIS_SERVICE_REQ_METY>();
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

        public V_HIS_SERVICE_REQ_METY GetViewById(long id, HisServiceReqMetySO search)
        {
            V_HIS_SERVICE_REQ_METY result = null;

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
