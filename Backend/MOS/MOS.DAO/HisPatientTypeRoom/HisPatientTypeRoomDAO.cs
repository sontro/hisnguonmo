using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisPatientTypeRoom
{
    public partial class HisPatientTypeRoomDAO : EntityBase
    {
        private HisPatientTypeRoomCreate CreateWorker
        {
            get
            {
                return (HisPatientTypeRoomCreate)Worker.Get<HisPatientTypeRoomCreate>();
            }
        }
        private HisPatientTypeRoomUpdate UpdateWorker
        {
            get
            {
                return (HisPatientTypeRoomUpdate)Worker.Get<HisPatientTypeRoomUpdate>();
            }
        }
        private HisPatientTypeRoomDelete DeleteWorker
        {
            get
            {
                return (HisPatientTypeRoomDelete)Worker.Get<HisPatientTypeRoomDelete>();
            }
        }
        private HisPatientTypeRoomTruncate TruncateWorker
        {
            get
            {
                return (HisPatientTypeRoomTruncate)Worker.Get<HisPatientTypeRoomTruncate>();
            }
        }
        private HisPatientTypeRoomGet GetWorker
        {
            get
            {
                return (HisPatientTypeRoomGet)Worker.Get<HisPatientTypeRoomGet>();
            }
        }
        private HisPatientTypeRoomCheck CheckWorker
        {
            get
            {
                return (HisPatientTypeRoomCheck)Worker.Get<HisPatientTypeRoomCheck>();
            }
        }

        public bool Create(HIS_PATIENT_TYPE_ROOM data)
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

        public bool CreateList(List<HIS_PATIENT_TYPE_ROOM> listData)
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

        public bool Update(HIS_PATIENT_TYPE_ROOM data)
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

        public bool UpdateList(List<HIS_PATIENT_TYPE_ROOM> listData)
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

        public bool Delete(HIS_PATIENT_TYPE_ROOM data)
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

        public bool DeleteList(List<HIS_PATIENT_TYPE_ROOM> listData)
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

        public bool Truncate(HIS_PATIENT_TYPE_ROOM data)
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

        public bool TruncateList(List<HIS_PATIENT_TYPE_ROOM> listData)
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

        public List<HIS_PATIENT_TYPE_ROOM> Get(HisPatientTypeRoomSO search, CommonParam param)
        {
            List<HIS_PATIENT_TYPE_ROOM> result = new List<HIS_PATIENT_TYPE_ROOM>();
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

        public HIS_PATIENT_TYPE_ROOM GetById(long id, HisPatientTypeRoomSO search)
        {
            HIS_PATIENT_TYPE_ROOM result = null;
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
