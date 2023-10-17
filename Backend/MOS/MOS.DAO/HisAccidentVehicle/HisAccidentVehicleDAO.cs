using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAccidentVehicle
{
    public partial class HisAccidentVehicleDAO : EntityBase
    {
        private HisAccidentVehicleCreate CreateWorker
        {
            get
            {
                return (HisAccidentVehicleCreate)Worker.Get<HisAccidentVehicleCreate>();
            }
        }
        private HisAccidentVehicleUpdate UpdateWorker
        {
            get
            {
                return (HisAccidentVehicleUpdate)Worker.Get<HisAccidentVehicleUpdate>();
            }
        }
        private HisAccidentVehicleDelete DeleteWorker
        {
            get
            {
                return (HisAccidentVehicleDelete)Worker.Get<HisAccidentVehicleDelete>();
            }
        }
        private HisAccidentVehicleTruncate TruncateWorker
        {
            get
            {
                return (HisAccidentVehicleTruncate)Worker.Get<HisAccidentVehicleTruncate>();
            }
        }
        private HisAccidentVehicleGet GetWorker
        {
            get
            {
                return (HisAccidentVehicleGet)Worker.Get<HisAccidentVehicleGet>();
            }
        }
        private HisAccidentVehicleCheck CheckWorker
        {
            get
            {
                return (HisAccidentVehicleCheck)Worker.Get<HisAccidentVehicleCheck>();
            }
        }

        public bool Create(HIS_ACCIDENT_VEHICLE data)
        {
            bool result = false;
            try
            {
                result = CreateWorker.Create(data);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public bool CreateList(List<HIS_ACCIDENT_VEHICLE> listData)
        {
            bool result = false;
            try
            {
                result = CreateWorker.CreateList(listData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public bool Update(HIS_ACCIDENT_VEHICLE data)
        {
            bool result = false;
            try
            {
                result = UpdateWorker.Update(data);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public bool UpdateList(List<HIS_ACCIDENT_VEHICLE> listData)
        {
            bool result = false;
            try
            {
                result = UpdateWorker.UpdateList(listData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public bool Delete(HIS_ACCIDENT_VEHICLE data)
        {
            bool result = false;
            try
            {
                result = DeleteWorker.Delete(data);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public bool DeleteList(List<HIS_ACCIDENT_VEHICLE> listData)
        {
            bool result = false;

            try
            {
                result = DeleteWorker.DeleteList(listData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public bool Truncate(HIS_ACCIDENT_VEHICLE data)
        {
            bool result = false;
            try
            {
                result = TruncateWorker.Truncate(data);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public bool TruncateList(List<HIS_ACCIDENT_VEHICLE> listData)
        {
            bool result = false;
            try
            {
                result = TruncateWorker.TruncateList(listData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public List<HIS_ACCIDENT_VEHICLE> Get(HisAccidentVehicleSO search, CommonParam param)
        {
            List<HIS_ACCIDENT_VEHICLE> result = new List<HIS_ACCIDENT_VEHICLE>();
            try
            {
                result = GetWorker.Get(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }
            return result;
        }

        public HIS_ACCIDENT_VEHICLE GetById(long id, HisAccidentVehicleSO search)
        {
            HIS_ACCIDENT_VEHICLE result = null;
            try
            {
                result = GetWorker.GetById(id, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }

        public bool IsUnLock(long id)
        {
            try
            {
                return CheckWorker.IsUnLock(id);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                throw;
            }
        }
    }
}
