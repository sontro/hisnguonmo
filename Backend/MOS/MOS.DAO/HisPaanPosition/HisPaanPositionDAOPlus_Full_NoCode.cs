using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisPaanPosition
{
    public partial class HisPaanPositionDAO : EntityBase
    {
        public List<V_HIS_PAAN_POSITION> GetView(HisPaanPositionSO search, CommonParam param)
        {
            List<V_HIS_PAAN_POSITION> result = new List<V_HIS_PAAN_POSITION>();
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

        public V_HIS_PAAN_POSITION GetViewById(long id, HisPaanPositionSO search)
        {
            V_HIS_PAAN_POSITION result = null;

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
