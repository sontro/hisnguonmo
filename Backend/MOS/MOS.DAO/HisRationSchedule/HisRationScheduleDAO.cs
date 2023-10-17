using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisRationSchedule
{
    public partial class HisRationScheduleDAO : EntityBase
    {
        private HisRationScheduleCreate CreateWorker
        {
            get
            {
                return (HisRationScheduleCreate)Worker.Get<HisRationScheduleCreate>();
            }
        }
        private HisRationScheduleUpdate UpdateWorker
        {
            get
            {
                return (HisRationScheduleUpdate)Worker.Get<HisRationScheduleUpdate>();
            }
        }
        private HisRationScheduleDelete DeleteWorker
        {
            get
            {
                return (HisRationScheduleDelete)Worker.Get<HisRationScheduleDelete>();
            }
        }
        private HisRationScheduleTruncate TruncateWorker
        {
            get
            {
                return (HisRationScheduleTruncate)Worker.Get<HisRationScheduleTruncate>();
            }
        }
        private HisRationScheduleGet GetWorker
        {
            get
            {
                return (HisRationScheduleGet)Worker.Get<HisRationScheduleGet>();
            }
        }
        private HisRationScheduleCheck CheckWorker
        {
            get
            {
                return (HisRationScheduleCheck)Worker.Get<HisRationScheduleCheck>();
            }
        }

        public bool Create(HIS_RATION_SCHEDULE data)
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

        public bool CreateList(List<HIS_RATION_SCHEDULE> listData)
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

        public bool Update(HIS_RATION_SCHEDULE data)
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

        public bool UpdateList(List<HIS_RATION_SCHEDULE> listData)
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

        public bool Delete(HIS_RATION_SCHEDULE data)
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

        public bool DeleteList(List<HIS_RATION_SCHEDULE> listData)
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

        public bool Truncate(HIS_RATION_SCHEDULE data)
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

        public bool TruncateList(List<HIS_RATION_SCHEDULE> listData)
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

        public List<HIS_RATION_SCHEDULE> Get(HisRationScheduleSO search, CommonParam param)
        {
            List<HIS_RATION_SCHEDULE> result = new List<HIS_RATION_SCHEDULE>();
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

        public HIS_RATION_SCHEDULE GetById(long id, HisRationScheduleSO search)
        {
            HIS_RATION_SCHEDULE result = null;
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
