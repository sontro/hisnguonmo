using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisPeriodDriverDity
{
    public partial class HisPeriodDriverDityDAO : EntityBase
    {
        private HisPeriodDriverDityCreate CreateWorker
        {
            get
            {
                return (HisPeriodDriverDityCreate)Worker.Get<HisPeriodDriverDityCreate>();
            }
        }
        private HisPeriodDriverDityUpdate UpdateWorker
        {
            get
            {
                return (HisPeriodDriverDityUpdate)Worker.Get<HisPeriodDriverDityUpdate>();
            }
        }
        private HisPeriodDriverDityDelete DeleteWorker
        {
            get
            {
                return (HisPeriodDriverDityDelete)Worker.Get<HisPeriodDriverDityDelete>();
            }
        }
        private HisPeriodDriverDityTruncate TruncateWorker
        {
            get
            {
                return (HisPeriodDriverDityTruncate)Worker.Get<HisPeriodDriverDityTruncate>();
            }
        }
        private HisPeriodDriverDityGet GetWorker
        {
            get
            {
                return (HisPeriodDriverDityGet)Worker.Get<HisPeriodDriverDityGet>();
            }
        }
        private HisPeriodDriverDityCheck CheckWorker
        {
            get
            {
                return (HisPeriodDriverDityCheck)Worker.Get<HisPeriodDriverDityCheck>();
            }
        }

        public bool Create(HIS_PERIOD_DRIVER_DITY data)
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

        public bool CreateList(List<HIS_PERIOD_DRIVER_DITY> listData)
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

        public bool Update(HIS_PERIOD_DRIVER_DITY data)
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

        public bool UpdateList(List<HIS_PERIOD_DRIVER_DITY> listData)
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

        public bool Delete(HIS_PERIOD_DRIVER_DITY data)
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

        public bool DeleteList(List<HIS_PERIOD_DRIVER_DITY> listData)
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

        public bool Truncate(HIS_PERIOD_DRIVER_DITY data)
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

        public bool TruncateList(List<HIS_PERIOD_DRIVER_DITY> listData)
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

        public List<HIS_PERIOD_DRIVER_DITY> Get(HisPeriodDriverDitySO search, CommonParam param)
        {
            List<HIS_PERIOD_DRIVER_DITY> result = new List<HIS_PERIOD_DRIVER_DITY>();
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

        public HIS_PERIOD_DRIVER_DITY GetById(long id, HisPeriodDriverDitySO search)
        {
            HIS_PERIOD_DRIVER_DITY result = null;
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
