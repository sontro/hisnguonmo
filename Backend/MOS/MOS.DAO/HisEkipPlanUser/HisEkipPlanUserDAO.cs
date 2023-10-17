using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisEkipPlanUser
{
    public partial class HisEkipPlanUserDAO : EntityBase
    {
        private HisEkipPlanUserCreate CreateWorker
        {
            get
            {
                return (HisEkipPlanUserCreate)Worker.Get<HisEkipPlanUserCreate>();
            }
        }
        private HisEkipPlanUserUpdate UpdateWorker
        {
            get
            {
                return (HisEkipPlanUserUpdate)Worker.Get<HisEkipPlanUserUpdate>();
            }
        }
        private HisEkipPlanUserDelete DeleteWorker
        {
            get
            {
                return (HisEkipPlanUserDelete)Worker.Get<HisEkipPlanUserDelete>();
            }
        }
        private HisEkipPlanUserTruncate TruncateWorker
        {
            get
            {
                return (HisEkipPlanUserTruncate)Worker.Get<HisEkipPlanUserTruncate>();
            }
        }
        private HisEkipPlanUserGet GetWorker
        {
            get
            {
                return (HisEkipPlanUserGet)Worker.Get<HisEkipPlanUserGet>();
            }
        }
        private HisEkipPlanUserCheck CheckWorker
        {
            get
            {
                return (HisEkipPlanUserCheck)Worker.Get<HisEkipPlanUserCheck>();
            }
        }

        public bool Create(HIS_EKIP_PLAN_USER data)
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

        public bool CreateList(List<HIS_EKIP_PLAN_USER> listData)
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

        public bool Update(HIS_EKIP_PLAN_USER data)
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

        public bool UpdateList(List<HIS_EKIP_PLAN_USER> listData)
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

        public bool Delete(HIS_EKIP_PLAN_USER data)
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

        public bool DeleteList(List<HIS_EKIP_PLAN_USER> listData)
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

        public bool Truncate(HIS_EKIP_PLAN_USER data)
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

        public bool TruncateList(List<HIS_EKIP_PLAN_USER> listData)
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

        public List<HIS_EKIP_PLAN_USER> Get(HisEkipPlanUserSO search, CommonParam param)
        {
            List<HIS_EKIP_PLAN_USER> result = new List<HIS_EKIP_PLAN_USER>();
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

        public HIS_EKIP_PLAN_USER GetById(long id, HisEkipPlanUserSO search)
        {
            HIS_EKIP_PLAN_USER result = null;
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
