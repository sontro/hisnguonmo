using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisVaccAppointment
{
    public partial class HisVaccAppointmentDAO : EntityBase
    {
        private HisVaccAppointmentCreate CreateWorker
        {
            get
            {
                return (HisVaccAppointmentCreate)Worker.Get<HisVaccAppointmentCreate>();
            }
        }
        private HisVaccAppointmentUpdate UpdateWorker
        {
            get
            {
                return (HisVaccAppointmentUpdate)Worker.Get<HisVaccAppointmentUpdate>();
            }
        }
        private HisVaccAppointmentDelete DeleteWorker
        {
            get
            {
                return (HisVaccAppointmentDelete)Worker.Get<HisVaccAppointmentDelete>();
            }
        }
        private HisVaccAppointmentTruncate TruncateWorker
        {
            get
            {
                return (HisVaccAppointmentTruncate)Worker.Get<HisVaccAppointmentTruncate>();
            }
        }
        private HisVaccAppointmentGet GetWorker
        {
            get
            {
                return (HisVaccAppointmentGet)Worker.Get<HisVaccAppointmentGet>();
            }
        }
        private HisVaccAppointmentCheck CheckWorker
        {
            get
            {
                return (HisVaccAppointmentCheck)Worker.Get<HisVaccAppointmentCheck>();
            }
        }

        public bool Create(HIS_VACC_APPOINTMENT data)
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

        public bool CreateList(List<HIS_VACC_APPOINTMENT> listData)
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

        public bool Update(HIS_VACC_APPOINTMENT data)
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

        public bool UpdateList(List<HIS_VACC_APPOINTMENT> listData)
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

        public bool Delete(HIS_VACC_APPOINTMENT data)
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

        public bool DeleteList(List<HIS_VACC_APPOINTMENT> listData)
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

        public bool Truncate(HIS_VACC_APPOINTMENT data)
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

        public bool TruncateList(List<HIS_VACC_APPOINTMENT> listData)
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

        public List<HIS_VACC_APPOINTMENT> Get(HisVaccAppointmentSO search, CommonParam param)
        {
            List<HIS_VACC_APPOINTMENT> result = new List<HIS_VACC_APPOINTMENT>();
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

        public HIS_VACC_APPOINTMENT GetById(long id, HisVaccAppointmentSO search)
        {
            HIS_VACC_APPOINTMENT result = null;
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
