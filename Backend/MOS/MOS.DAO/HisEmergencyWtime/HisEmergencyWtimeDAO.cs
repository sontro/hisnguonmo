using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisEmergencyWtime
{
    public partial class HisEmergencyWtimeDAO : EntityBase
    {
        private HisEmergencyWtimeCreate CreateWorker
        {
            get
            {
                return (HisEmergencyWtimeCreate)Worker.Get<HisEmergencyWtimeCreate>();
            }
        }
        private HisEmergencyWtimeUpdate UpdateWorker
        {
            get
            {
                return (HisEmergencyWtimeUpdate)Worker.Get<HisEmergencyWtimeUpdate>();
            }
        }
        private HisEmergencyWtimeDelete DeleteWorker
        {
            get
            {
                return (HisEmergencyWtimeDelete)Worker.Get<HisEmergencyWtimeDelete>();
            }
        }
        private HisEmergencyWtimeTruncate TruncateWorker
        {
            get
            {
                return (HisEmergencyWtimeTruncate)Worker.Get<HisEmergencyWtimeTruncate>();
            }
        }
        private HisEmergencyWtimeGet GetWorker
        {
            get
            {
                return (HisEmergencyWtimeGet)Worker.Get<HisEmergencyWtimeGet>();
            }
        }
        private HisEmergencyWtimeCheck CheckWorker
        {
            get
            {
                return (HisEmergencyWtimeCheck)Worker.Get<HisEmergencyWtimeCheck>();
            }
        }

        public bool Create(HIS_EMERGENCY_WTIME data)
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

        public bool CreateList(List<HIS_EMERGENCY_WTIME> listData)
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

        public bool Update(HIS_EMERGENCY_WTIME data)
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

        public bool UpdateList(List<HIS_EMERGENCY_WTIME> listData)
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

        public bool Delete(HIS_EMERGENCY_WTIME data)
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

        public bool DeleteList(List<HIS_EMERGENCY_WTIME> listData)
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

        public bool Truncate(HIS_EMERGENCY_WTIME data)
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

        public bool TruncateList(List<HIS_EMERGENCY_WTIME> listData)
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

        public List<HIS_EMERGENCY_WTIME> Get(HisEmergencyWtimeSO search, CommonParam param)
        {
            List<HIS_EMERGENCY_WTIME> result = new List<HIS_EMERGENCY_WTIME>();
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

        public HIS_EMERGENCY_WTIME GetById(long id, HisEmergencyWtimeSO search)
        {
            HIS_EMERGENCY_WTIME result = null;
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
