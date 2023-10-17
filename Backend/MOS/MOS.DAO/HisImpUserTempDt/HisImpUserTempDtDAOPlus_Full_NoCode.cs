using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisImpUserTempDt
{
    public partial class HisImpUserTempDtDAO : EntityBase
    {
        public List<V_HIS_IMP_USER_TEMP_DT> GetView(HisImpUserTempDtSO search, CommonParam param)
        {
            List<V_HIS_IMP_USER_TEMP_DT> result = new List<V_HIS_IMP_USER_TEMP_DT>();
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

        public V_HIS_IMP_USER_TEMP_DT GetViewById(long id, HisImpUserTempDtSO search)
        {
            V_HIS_IMP_USER_TEMP_DT result = null;

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
