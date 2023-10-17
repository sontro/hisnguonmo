using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisPetroleum
{
    public partial class HisPetroleumDAO : EntityBase
    {
        public List<V_HIS_PETROLEUM> GetView(HisPetroleumSO search, CommonParam param)
        {
            List<V_HIS_PETROLEUM> result = new List<V_HIS_PETROLEUM>();
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

        public V_HIS_PETROLEUM GetViewById(long id, HisPetroleumSO search)
        {
            V_HIS_PETROLEUM result = null;

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
