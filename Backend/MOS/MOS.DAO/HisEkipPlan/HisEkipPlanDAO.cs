using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisEkipPlan
{
    public partial class HisEkipPlanDAO : EntityBase
    {
        private HisEkipPlanCreate CreateWorker
        {
            get
            {
                return (HisEkipPlanCreate)Worker.Get<HisEkipPlanCreate>();
            }
        }
        private HisEkipPlanUpdate UpdateWorker
        {
            get
            {
                return (HisEkipPlanUpdate)Worker.Get<HisEkipPlanUpdate>();
            }
        }
        private HisEkipPlanDelete DeleteWorker
        {
            get
            {
                return (HisEkipPlanDelete)Worker.Get<HisEkipPlanDelete>();
            }
        }
        private HisEkipPlanTruncate TruncateWorker
        {
            get
            {
                return (HisEkipPlanTruncate)Worker.Get<HisEkipPlanTruncate>();
            }
        }
        private HisEkipPlanGet GetWorker
        {
            get
            {
                return (HisEkipPlanGet)Worker.Get<HisEkipPlanGet>();
            }
        }
        private HisEkipPlanCheck CheckWorker
        {
            get
            {
                return (HisEkipPlanCheck)Worker.Get<HisEkipPlanCheck>();
            }
        }

        public bool Create(HIS_EKIP_PLAN data)
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

        public bool CreateList(List<HIS_EKIP_PLAN> listData)
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

        public bool Update(HIS_EKIP_PLAN data)
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

        public bool UpdateList(List<HIS_EKIP_PLAN> listData)
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

        public bool Delete(HIS_EKIP_PLAN data)
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

        public bool DeleteList(List<HIS_EKIP_PLAN> listData)
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

        public bool Truncate(HIS_EKIP_PLAN data)
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

        public bool TruncateList(List<HIS_EKIP_PLAN> listData)
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

        public List<HIS_EKIP_PLAN> Get(HisEkipPlanSO search, CommonParam param)
        {
            List<HIS_EKIP_PLAN> result = new List<HIS_EKIP_PLAN>();
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

        public HIS_EKIP_PLAN GetById(long id, HisEkipPlanSO search)
        {
            HIS_EKIP_PLAN result = null;
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
