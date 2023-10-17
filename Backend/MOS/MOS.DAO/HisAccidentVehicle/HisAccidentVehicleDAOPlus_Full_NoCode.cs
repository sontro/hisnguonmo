using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAccidentVehicle
{
    public partial class HisAccidentVehicleDAO : EntityBase
    {
        public List<V_HIS_ACCIDENT_VEHICLE> GetView(HisAccidentVehicleSO search, CommonParam param)
        {
            List<V_HIS_ACCIDENT_VEHICLE> result = new List<V_HIS_ACCIDENT_VEHICLE>();
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

        public V_HIS_ACCIDENT_VEHICLE GetViewById(long id, HisAccidentVehicleSO search)
        {
            V_HIS_ACCIDENT_VEHICLE result = null;

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
