using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAppointmentServ
{
    public partial class HisAppointmentServDAO : EntityBase
    {
        private HisAppointmentServCreate CreateWorker
        {
            get
            {
                return (HisAppointmentServCreate)Worker.Get<HisAppointmentServCreate>();
            }
        }
        private HisAppointmentServUpdate UpdateWorker
        {
            get
            {
                return (HisAppointmentServUpdate)Worker.Get<HisAppointmentServUpdate>();
            }
        }
        private HisAppointmentServDelete DeleteWorker
        {
            get
            {
                return (HisAppointmentServDelete)Worker.Get<HisAppointmentServDelete>();
            }
        }
        private HisAppointmentServTruncate TruncateWorker
        {
            get
            {
                return (HisAppointmentServTruncate)Worker.Get<HisAppointmentServTruncate>();
            }
        }
        private HisAppointmentServGet GetWorker
        {
            get
            {
                return (HisAppointmentServGet)Worker.Get<HisAppointmentServGet>();
            }
        }
        private HisAppointmentServCheck CheckWorker
        {
            get
            {
                return (HisAppointmentServCheck)Worker.Get<HisAppointmentServCheck>();
            }
        }

        public bool Create(HIS_APPOINTMENT_SERV data)
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

        public bool CreateList(List<HIS_APPOINTMENT_SERV> listData)
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

        public bool Update(HIS_APPOINTMENT_SERV data)
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

        public bool UpdateList(List<HIS_APPOINTMENT_SERV> listData)
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

        public bool Delete(HIS_APPOINTMENT_SERV data)
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

        public bool DeleteList(List<HIS_APPOINTMENT_SERV> listData)
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

        public bool Truncate(HIS_APPOINTMENT_SERV data)
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

        public bool TruncateList(List<HIS_APPOINTMENT_SERV> listData)
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

        public List<HIS_APPOINTMENT_SERV> Get(HisAppointmentServSO search, CommonParam param)
        {
            List<HIS_APPOINTMENT_SERV> result = new List<HIS_APPOINTMENT_SERV>();
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

        public HIS_APPOINTMENT_SERV GetById(long id, HisAppointmentServSO search)
        {
            HIS_APPOINTMENT_SERV result = null;
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
