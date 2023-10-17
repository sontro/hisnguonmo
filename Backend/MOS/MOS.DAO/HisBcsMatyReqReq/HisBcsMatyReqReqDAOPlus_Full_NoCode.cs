using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBcsMatyReqReq
{
    public partial class HisBcsMatyReqReqDAO : EntityBase
    {
        public List<V_HIS_BCS_MATY_REQ_REQ> GetView(HisBcsMatyReqReqSO search, CommonParam param)
        {
            List<V_HIS_BCS_MATY_REQ_REQ> result = new List<V_HIS_BCS_MATY_REQ_REQ>();
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

        public V_HIS_BCS_MATY_REQ_REQ GetViewById(long id, HisBcsMatyReqReqSO search)
        {
            V_HIS_BCS_MATY_REQ_REQ result = null;

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
