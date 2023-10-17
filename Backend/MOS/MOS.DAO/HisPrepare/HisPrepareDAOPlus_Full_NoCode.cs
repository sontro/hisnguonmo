using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisPrepare
{
    public partial class HisPrepareDAO : EntityBase
    {
        public List<V_HIS_PREPARE> GetView(HisPrepareSO search, CommonParam param)
        {
            List<V_HIS_PREPARE> result = new List<V_HIS_PREPARE>();
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

        public V_HIS_PREPARE GetViewById(long id, HisPrepareSO search)
        {
            V_HIS_PREPARE result = null;

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
