using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAppointmentPeriod
{
    public partial class HisAppointmentPeriodDAO : EntityBase
    {
        private HisAppointmentPeriodCreate CreateWorker
        {
            get
            {
                return (HisAppointmentPeriodCreate)Worker.Get<HisAppointmentPeriodCreate>();
            }
        }
        private HisAppointmentPeriodUpdate UpdateWorker
        {
            get
            {
                return (HisAppointmentPeriodUpdate)Worker.Get<HisAppointmentPeriodUpdate>();
            }
        }
        private HisAppointmentPeriodDelete DeleteWorker
        {
            get
            {
                return (HisAppointmentPeriodDelete)Worker.Get<HisAppointmentPeriodDelete>();
            }
        }
        private HisAppointmentPeriodTruncate TruncateWorker
        {
            get
            {
                return (HisAppointmentPeriodTruncate)Worker.Get<HisAppointmentPeriodTruncate>();
            }
        }
        private HisAppointmentPeriodGet GetWorker
        {
            get
            {
                return (HisAppointmentPeriodGet)Worker.Get<HisAppointmentPeriodGet>();
            }
        }
        private HisAppointmentPeriodCheck CheckWorker
        {
            get
            {
                return (HisAppointmentPeriodCheck)Worker.Get<HisAppointmentPeriodCheck>();
            }
        }

        public bool Create(HIS_APPOINTMENT_PERIOD data)
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

        public bool CreateList(List<HIS_APPOINTMENT_PERIOD> listData)
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

        public bool Update(HIS_APPOINTMENT_PERIOD data)
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

        public bool UpdateList(List<HIS_APPOINTMENT_PERIOD> listData)
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

        public bool Delete(HIS_APPOINTMENT_PERIOD data)
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

        public bool DeleteList(List<HIS_APPOINTMENT_PERIOD> listData)
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

        public bool Truncate(HIS_APPOINTMENT_PERIOD data)
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

        public bool TruncateList(List<HIS_APPOINTMENT_PERIOD> listData)
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

        public List<HIS_APPOINTMENT_PERIOD> Get(HisAppointmentPeriodSO search, CommonParam param)
        {
            List<HIS_APPOINTMENT_PERIOD> result = new List<HIS_APPOINTMENT_PERIOD>();
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

        public HIS_APPOINTMENT_PERIOD GetById(long id, HisAppointmentPeriodSO search)
        {
            HIS_APPOINTMENT_PERIOD result = null;
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
