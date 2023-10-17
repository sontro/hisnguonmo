using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMedicineTypeRoom
{
    public partial class HisMedicineTypeRoomDAO : EntityBase
    {
        private HisMedicineTypeRoomCreate CreateWorker
        {
            get
            {
                return (HisMedicineTypeRoomCreate)Worker.Get<HisMedicineTypeRoomCreate>();
            }
        }
        private HisMedicineTypeRoomUpdate UpdateWorker
        {
            get
            {
                return (HisMedicineTypeRoomUpdate)Worker.Get<HisMedicineTypeRoomUpdate>();
            }
        }
        private HisMedicineTypeRoomDelete DeleteWorker
        {
            get
            {
                return (HisMedicineTypeRoomDelete)Worker.Get<HisMedicineTypeRoomDelete>();
            }
        }
        private HisMedicineTypeRoomTruncate TruncateWorker
        {
            get
            {
                return (HisMedicineTypeRoomTruncate)Worker.Get<HisMedicineTypeRoomTruncate>();
            }
        }
        private HisMedicineTypeRoomGet GetWorker
        {
            get
            {
                return (HisMedicineTypeRoomGet)Worker.Get<HisMedicineTypeRoomGet>();
            }
        }
        private HisMedicineTypeRoomCheck CheckWorker
        {
            get
            {
                return (HisMedicineTypeRoomCheck)Worker.Get<HisMedicineTypeRoomCheck>();
            }
        }

        public bool Create(HIS_MEDICINE_TYPE_ROOM data)
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

        public bool CreateList(List<HIS_MEDICINE_TYPE_ROOM> listData)
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

        public bool Update(HIS_MEDICINE_TYPE_ROOM data)
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

        public bool UpdateList(List<HIS_MEDICINE_TYPE_ROOM> listData)
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

        public bool Delete(HIS_MEDICINE_TYPE_ROOM data)
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

        public bool DeleteList(List<HIS_MEDICINE_TYPE_ROOM> listData)
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

        public bool Truncate(HIS_MEDICINE_TYPE_ROOM data)
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

        public bool TruncateList(List<HIS_MEDICINE_TYPE_ROOM> listData)
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

        public List<HIS_MEDICINE_TYPE_ROOM> Get(HisMedicineTypeRoomSO search, CommonParam param)
        {
            List<HIS_MEDICINE_TYPE_ROOM> result = new List<HIS_MEDICINE_TYPE_ROOM>();
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

        public HIS_MEDICINE_TYPE_ROOM GetById(long id, HisMedicineTypeRoomSO search)
        {
            HIS_MEDICINE_TYPE_ROOM result = null;
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
