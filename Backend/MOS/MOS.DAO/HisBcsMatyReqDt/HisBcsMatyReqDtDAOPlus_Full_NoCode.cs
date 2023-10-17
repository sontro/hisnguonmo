using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBcsMatyReqDt
{
    public partial class HisBcsMatyReqDtDAO : EntityBase
    {
        public List<V_HIS_BCS_MATY_REQ_DT> GetView(HisBcsMatyReqDtSO search, CommonParam param)
        {
            List<V_HIS_BCS_MATY_REQ_DT> result = new List<V_HIS_BCS_MATY_REQ_DT>();
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

        public V_HIS_BCS_MATY_REQ_DT GetViewById(long id, HisBcsMatyReqDtSO search)
        {
            V_HIS_BCS_MATY_REQ_DT result = null;

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
