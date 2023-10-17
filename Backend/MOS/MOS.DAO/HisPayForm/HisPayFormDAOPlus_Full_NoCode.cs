using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisPayForm
{
    public partial class HisPayFormDAO : EntityBase
    {
        public List<V_HIS_PAY_FORM> GetView(HisPayFormSO search, CommonParam param)
        {
            List<V_HIS_PAY_FORM> result = new List<V_HIS_PAY_FORM>();
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

        public V_HIS_PAY_FORM GetViewById(long id, HisPayFormSO search)
        {
            V_HIS_PAY_FORM result = null;

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
