using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisDepositReq
{
    public partial class HisDepositReqDAO : EntityBase
    {
        public List<V_HIS_DEPOSIT_REQ> GetView(HisDepositReqSO search, CommonParam param)
        {
            List<V_HIS_DEPOSIT_REQ> result = new List<V_HIS_DEPOSIT_REQ>();
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

        public V_HIS_DEPOSIT_REQ GetViewById(long id, HisDepositReqSO search)
        {
            V_HIS_DEPOSIT_REQ result = null;

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
