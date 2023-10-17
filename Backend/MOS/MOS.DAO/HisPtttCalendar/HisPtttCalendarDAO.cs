using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisPtttCalendar
{
    public partial class HisPtttCalendarDAO : EntityBase
    {
        private HisPtttCalendarCreate CreateWorker
        {
            get
            {
                return (HisPtttCalendarCreate)Worker.Get<HisPtttCalendarCreate>();
            }
        }
        private HisPtttCalendarUpdate UpdateWorker
        {
            get
            {
                return (HisPtttCalendarUpdate)Worker.Get<HisPtttCalendarUpdate>();
            }
        }
        private HisPtttCalendarDelete DeleteWorker
        {
            get
            {
                return (HisPtttCalendarDelete)Worker.Get<HisPtttCalendarDelete>();
            }
        }
        private HisPtttCalendarTruncate TruncateWorker
        {
            get
            {
                return (HisPtttCalendarTruncate)Worker.Get<HisPtttCalendarTruncate>();
            }
        }
        private HisPtttCalendarGet GetWorker
        {
            get
            {
                return (HisPtttCalendarGet)Worker.Get<HisPtttCalendarGet>();
            }
        }
        private HisPtttCalendarCheck CheckWorker
        {
            get
            {
                return (HisPtttCalendarCheck)Worker.Get<HisPtttCalendarCheck>();
            }
        }

        public bool Create(HIS_PTTT_CALENDAR data)
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

        public bool CreateList(List<HIS_PTTT_CALENDAR> listData)
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

        public bool Update(HIS_PTTT_CALENDAR data)
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

        public bool UpdateList(List<HIS_PTTT_CALENDAR> listData)
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

        public bool Delete(HIS_PTTT_CALENDAR data)
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

        public bool DeleteList(List<HIS_PTTT_CALENDAR> listData)
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

        public bool Truncate(HIS_PTTT_CALENDAR data)
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

        public bool TruncateList(List<HIS_PTTT_CALENDAR> listData)
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

        public List<HIS_PTTT_CALENDAR> Get(HisPtttCalendarSO search, CommonParam param)
        {
            List<HIS_PTTT_CALENDAR> result = new List<HIS_PTTT_CALENDAR>();
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

        public HIS_PTTT_CALENDAR GetById(long id, HisPtttCalendarSO search)
        {
            HIS_PTTT_CALENDAR result = null;
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
