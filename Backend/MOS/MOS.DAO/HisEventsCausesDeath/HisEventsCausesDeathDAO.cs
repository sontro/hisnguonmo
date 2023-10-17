using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisEventsCausesDeath
{
    public partial class HisEventsCausesDeathDAO : EntityBase
    {
        private HisEventsCausesDeathCreate CreateWorker
        {
            get
            {
                return (HisEventsCausesDeathCreate)Worker.Get<HisEventsCausesDeathCreate>();
            }
        }
        private HisEventsCausesDeathUpdate UpdateWorker
        {
            get
            {
                return (HisEventsCausesDeathUpdate)Worker.Get<HisEventsCausesDeathUpdate>();
            }
        }
        private HisEventsCausesDeathDelete DeleteWorker
        {
            get
            {
                return (HisEventsCausesDeathDelete)Worker.Get<HisEventsCausesDeathDelete>();
            }
        }
        private HisEventsCausesDeathTruncate TruncateWorker
        {
            get
            {
                return (HisEventsCausesDeathTruncate)Worker.Get<HisEventsCausesDeathTruncate>();
            }
        }
        private HisEventsCausesDeathGet GetWorker
        {
            get
            {
                return (HisEventsCausesDeathGet)Worker.Get<HisEventsCausesDeathGet>();
            }
        }
        private HisEventsCausesDeathCheck CheckWorker
        {
            get
            {
                return (HisEventsCausesDeathCheck)Worker.Get<HisEventsCausesDeathCheck>();
            }
        }

        public bool Create(HIS_EVENTS_CAUSES_DEATH data)
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

        public bool CreateList(List<HIS_EVENTS_CAUSES_DEATH> listData)
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

        public bool Update(HIS_EVENTS_CAUSES_DEATH data)
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

        public bool UpdateList(List<HIS_EVENTS_CAUSES_DEATH> listData)
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

        public bool Delete(HIS_EVENTS_CAUSES_DEATH data)
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

        public bool DeleteList(List<HIS_EVENTS_CAUSES_DEATH> listData)
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

        public bool Truncate(HIS_EVENTS_CAUSES_DEATH data)
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

        public bool TruncateList(List<HIS_EVENTS_CAUSES_DEATH> listData)
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

        public List<HIS_EVENTS_CAUSES_DEATH> Get(HisEventsCausesDeathSO search, CommonParam param)
        {
            List<HIS_EVENTS_CAUSES_DEATH> result = new List<HIS_EVENTS_CAUSES_DEATH>();
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

        public HIS_EVENTS_CAUSES_DEATH GetById(long id, HisEventsCausesDeathSO search)
        {
            HIS_EVENTS_CAUSES_DEATH result = null;
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
