using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisEmployeeSchedule
{
    public partial class HisEmployeeScheduleDAO : EntityBase
    {
        private HisEmployeeScheduleCreate CreateWorker
        {
            get
            {
                return (HisEmployeeScheduleCreate)Worker.Get<HisEmployeeScheduleCreate>();
            }
        }
        private HisEmployeeScheduleUpdate UpdateWorker
        {
            get
            {
                return (HisEmployeeScheduleUpdate)Worker.Get<HisEmployeeScheduleUpdate>();
            }
        }
        private HisEmployeeScheduleDelete DeleteWorker
        {
            get
            {
                return (HisEmployeeScheduleDelete)Worker.Get<HisEmployeeScheduleDelete>();
            }
        }
        private HisEmployeeScheduleTruncate TruncateWorker
        {
            get
            {
                return (HisEmployeeScheduleTruncate)Worker.Get<HisEmployeeScheduleTruncate>();
            }
        }
        private HisEmployeeScheduleGet GetWorker
        {
            get
            {
                return (HisEmployeeScheduleGet)Worker.Get<HisEmployeeScheduleGet>();
            }
        }
        private HisEmployeeScheduleCheck CheckWorker
        {
            get
            {
                return (HisEmployeeScheduleCheck)Worker.Get<HisEmployeeScheduleCheck>();
            }
        }

        public bool Create(HIS_EMPLOYEE_SCHEDULE data)
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

        public bool CreateList(List<HIS_EMPLOYEE_SCHEDULE> listData)
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

        public bool Update(HIS_EMPLOYEE_SCHEDULE data)
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

        public bool UpdateList(List<HIS_EMPLOYEE_SCHEDULE> listData)
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

        public bool Delete(HIS_EMPLOYEE_SCHEDULE data)
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

        public bool DeleteList(List<HIS_EMPLOYEE_SCHEDULE> listData)
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

        public bool Truncate(HIS_EMPLOYEE_SCHEDULE data)
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

        public bool TruncateList(List<HIS_EMPLOYEE_SCHEDULE> listData)
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

        public List<HIS_EMPLOYEE_SCHEDULE> Get(HisEmployeeScheduleSO search, CommonParam param)
        {
            List<HIS_EMPLOYEE_SCHEDULE> result = new List<HIS_EMPLOYEE_SCHEDULE>();
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

        public HIS_EMPLOYEE_SCHEDULE GetById(long id, HisEmployeeScheduleSO search)
        {
            HIS_EMPLOYEE_SCHEDULE result = null;
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
