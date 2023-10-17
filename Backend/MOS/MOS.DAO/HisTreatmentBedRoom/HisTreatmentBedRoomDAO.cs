using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisTreatmentBedRoom
{
    public partial class HisTreatmentBedRoomDAO : EntityBase
    {
        private HisTreatmentBedRoomCreate CreateWorker
        {
            get
            {
                return (HisTreatmentBedRoomCreate)Worker.Get<HisTreatmentBedRoomCreate>();
            }
        }
        private HisTreatmentBedRoomUpdate UpdateWorker
        {
            get
            {
                return (HisTreatmentBedRoomUpdate)Worker.Get<HisTreatmentBedRoomUpdate>();
            }
        }
        private HisTreatmentBedRoomDelete DeleteWorker
        {
            get
            {
                return (HisTreatmentBedRoomDelete)Worker.Get<HisTreatmentBedRoomDelete>();
            }
        }
        private HisTreatmentBedRoomTruncate TruncateWorker
        {
            get
            {
                return (HisTreatmentBedRoomTruncate)Worker.Get<HisTreatmentBedRoomTruncate>();
            }
        }
        private HisTreatmentBedRoomGet GetWorker
        {
            get
            {
                return (HisTreatmentBedRoomGet)Worker.Get<HisTreatmentBedRoomGet>();
            }
        }
        private HisTreatmentBedRoomCheck CheckWorker
        {
            get
            {
                return (HisTreatmentBedRoomCheck)Worker.Get<HisTreatmentBedRoomCheck>();
            }
        }

        public bool Create(HIS_TREATMENT_BED_ROOM data)
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

        public bool CreateList(List<HIS_TREATMENT_BED_ROOM> listData)
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

        public bool Update(HIS_TREATMENT_BED_ROOM data)
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

        public bool UpdateList(List<HIS_TREATMENT_BED_ROOM> listData)
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

        public bool Delete(HIS_TREATMENT_BED_ROOM data)
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

        public bool DeleteList(List<HIS_TREATMENT_BED_ROOM> listData)
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

        public bool Truncate(HIS_TREATMENT_BED_ROOM data)
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

        public bool TruncateList(List<HIS_TREATMENT_BED_ROOM> listData)
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

        public List<HIS_TREATMENT_BED_ROOM> Get(HisTreatmentBedRoomSO search, CommonParam param)
        {
            List<HIS_TREATMENT_BED_ROOM> result = new List<HIS_TREATMENT_BED_ROOM>();
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

        public HIS_TREATMENT_BED_ROOM GetById(long id, HisTreatmentBedRoomSO search)
        {
            HIS_TREATMENT_BED_ROOM result = null;
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
