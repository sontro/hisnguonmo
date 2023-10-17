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

        public HIS_ACCIDENT_VEHICLE GetByCode(string code, HisAccidentVehicleSO search)
        {
            HIS_ACCIDENT_VEHICLE result = null;

            try
            {
                result = GetWorker.GetByCode(code, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
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

        public V_HIS_ACCIDENT_VEHICLE GetViewByCode(string code, HisAccidentVehicleSO search)
        {
            V_HIS_ACCIDENT_VEHICLE result = null;

            try
            {
                result = GetWorker.GetViewByCode(code, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        public Dictionary<string, HIS_ACCIDENT_VEHICLE> GetDicByCode(HisAccidentVehicleSO search, CommonParam param)
        {
            Dictionary<string, HIS_ACCIDENT_VEHICLE> result = new Dictionary<string, HIS_ACCIDENT_VEHICLE>();
            try
            {
                result = GetWorker.GetDicByCode(search, param);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }

            return result;
        }

        public bool ExistsCode(string code, long? id)
        {
            try
            {
                return CheckWorker.ExistsCode(code, id);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                throw;
            }
        }
    }
}
