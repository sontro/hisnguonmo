using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBornPosition
{
    public partial class HisBornPositionDAO : EntityBase
    {
        public List<V_HIS_BORN_POSITION> GetView(HisBornPositionSO search, CommonParam param)
        {
            List<V_HIS_BORN_POSITION> result = new List<V_HIS_BORN_POSITION>();
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

        public V_HIS_BORN_POSITION GetViewById(long id, HisBornPositionSO search)
        {
            V_HIS_BORN_POSITION result = null;

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
