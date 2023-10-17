using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisDesk
{
    public partial class HisDeskDAO : EntityBase
    {
        public List<V_HIS_DESK> GetView(HisDeskSO search, CommonParam param)
        {
            List<V_HIS_DESK> result = new List<V_HIS_DESK>();
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

        public V_HIS_DESK GetViewById(long id, HisDeskSO search)
        {
            V_HIS_DESK result = null;

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
