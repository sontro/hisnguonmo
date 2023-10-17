using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisImpMestPay
{
    public partial class HisImpMestPayDAO : EntityBase
    {
        public List<V_HIS_IMP_MEST_PAY> GetView(HisImpMestPaySO search, CommonParam param)
        {
            List<V_HIS_IMP_MEST_PAY> result = new List<V_HIS_IMP_MEST_PAY>();
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

        public V_HIS_IMP_MEST_PAY GetViewById(long id, HisImpMestPaySO search)
        {
            V_HIS_IMP_MEST_PAY result = null;

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
