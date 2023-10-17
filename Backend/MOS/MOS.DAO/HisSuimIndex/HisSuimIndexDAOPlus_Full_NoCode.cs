using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSuimIndex
{
    public partial class HisSuimIndexDAO : EntityBase
    {
        public List<V_HIS_SUIM_INDEX> GetView(HisSuimIndexSO search, CommonParam param)
        {
            List<V_HIS_SUIM_INDEX> result = new List<V_HIS_SUIM_INDEX>();
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

        public V_HIS_SUIM_INDEX GetViewById(long id, HisSuimIndexSO search)
        {
            V_HIS_SUIM_INDEX result = null;

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
