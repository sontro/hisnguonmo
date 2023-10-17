using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBloodGiver
{
    public partial class HisBloodGiverDAO : EntityBase
    {
        public List<V_HIS_BLOOD_GIVER> GetView(HisBloodGiverSO search, CommonParam param)
        {
            List<V_HIS_BLOOD_GIVER> result = new List<V_HIS_BLOOD_GIVER>();
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

        public V_HIS_BLOOD_GIVER GetViewById(long id, HisBloodGiverSO search)
        {
            V_HIS_BLOOD_GIVER result = null;

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
