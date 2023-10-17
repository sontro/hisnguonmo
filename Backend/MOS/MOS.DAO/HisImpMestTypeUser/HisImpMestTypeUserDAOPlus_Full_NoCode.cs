using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisImpMestTypeUser
{
    public partial class HisImpMestTypeUserDAO : EntityBase
    {
        public List<V_HIS_IMP_MEST_TYPE_USER> GetView(HisImpMestTypeUserSO search, CommonParam param)
        {
            List<V_HIS_IMP_MEST_TYPE_USER> result = new List<V_HIS_IMP_MEST_TYPE_USER>();
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

        public V_HIS_IMP_MEST_TYPE_USER GetViewById(long id, HisImpMestTypeUserSO search)
        {
            V_HIS_IMP_MEST_TYPE_USER result = null;

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
