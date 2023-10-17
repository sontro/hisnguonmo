using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisRegimenHiv
{
    public partial class HisRegimenHivDAO : EntityBase
    {
        private HisRegimenHivCreate CreateWorker
        {
            get
            {
                return (HisRegimenHivCreate)Worker.Get<HisRegimenHivCreate>();
            }
        }
        private HisRegimenHivUpdate UpdateWorker
        {
            get
            {
                return (HisRegimenHivUpdate)Worker.Get<HisRegimenHivUpdate>();
            }
        }
        private HisRegimenHivDelete DeleteWorker
        {
            get
            {
                return (HisRegimenHivDelete)Worker.Get<HisRegimenHivDelete>();
            }
        }
        private HisRegimenHivTruncate TruncateWorker
        {
            get
            {
                return (HisRegimenHivTruncate)Worker.Get<HisRegimenHivTruncate>();
            }
        }
        private HisRegimenHivGet GetWorker
        {
            get
            {
                return (HisRegimenHivGet)Worker.Get<HisRegimenHivGet>();
            }
        }
        private HisRegimenHivCheck CheckWorker
        {
            get
            {
                return (HisRegimenHivCheck)Worker.Get<HisRegimenHivCheck>();
            }
        }

        public bool Create(HIS_REGIMEN_HIV data)
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

        public bool CreateList(List<HIS_REGIMEN_HIV> listData)
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

        public bool Update(HIS_REGIMEN_HIV data)
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

        public bool UpdateList(List<HIS_REGIMEN_HIV> listData)
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

        public bool Delete(HIS_REGIMEN_HIV data)
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

        public bool DeleteList(List<HIS_REGIMEN_HIV> listData)
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

        public bool Truncate(HIS_REGIMEN_HIV data)
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

        public bool TruncateList(List<HIS_REGIMEN_HIV> listData)
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

        public List<HIS_REGIMEN_HIV> Get(HisRegimenHivSO search, CommonParam param)
        {
            List<HIS_REGIMEN_HIV> result = new List<HIS_REGIMEN_HIV>();
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

        public HIS_REGIMEN_HIV GetById(long id, HisRegimenHivSO search)
        {
            HIS_REGIMEN_HIV result = null;
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
