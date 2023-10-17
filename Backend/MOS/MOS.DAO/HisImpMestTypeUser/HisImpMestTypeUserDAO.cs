using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisImpMestTypeUser
{
    public partial class HisImpMestTypeUserDAO : EntityBase
    {
        private HisImpMestTypeUserCreate CreateWorker
        {
            get
            {
                return (HisImpMestTypeUserCreate)Worker.Get<HisImpMestTypeUserCreate>();
            }
        }
        private HisImpMestTypeUserUpdate UpdateWorker
        {
            get
            {
                return (HisImpMestTypeUserUpdate)Worker.Get<HisImpMestTypeUserUpdate>();
            }
        }
        private HisImpMestTypeUserDelete DeleteWorker
        {
            get
            {
                return (HisImpMestTypeUserDelete)Worker.Get<HisImpMestTypeUserDelete>();
            }
        }
        private HisImpMestTypeUserTruncate TruncateWorker
        {
            get
            {
                return (HisImpMestTypeUserTruncate)Worker.Get<HisImpMestTypeUserTruncate>();
            }
        }
        private HisImpMestTypeUserGet GetWorker
        {
            get
            {
                return (HisImpMestTypeUserGet)Worker.Get<HisImpMestTypeUserGet>();
            }
        }
        private HisImpMestTypeUserCheck CheckWorker
        {
            get
            {
                return (HisImpMestTypeUserCheck)Worker.Get<HisImpMestTypeUserCheck>();
            }
        }

        public bool Create(HIS_IMP_MEST_TYPE_USER data)
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

        public bool CreateList(List<HIS_IMP_MEST_TYPE_USER> listData)
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

        public bool Update(HIS_IMP_MEST_TYPE_USER data)
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

        public bool UpdateList(List<HIS_IMP_MEST_TYPE_USER> listData)
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

        public bool Delete(HIS_IMP_MEST_TYPE_USER data)
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

        public bool DeleteList(List<HIS_IMP_MEST_TYPE_USER> listData)
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

        public bool Truncate(HIS_IMP_MEST_TYPE_USER data)
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

        public bool TruncateList(List<HIS_IMP_MEST_TYPE_USER> listData)
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

        public List<HIS_IMP_MEST_TYPE_USER> Get(HisImpMestTypeUserSO search, CommonParam param)
        {
            List<HIS_IMP_MEST_TYPE_USER> result = new List<HIS_IMP_MEST_TYPE_USER>();
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

        public HIS_IMP_MEST_TYPE_USER GetById(long id, HisImpMestTypeUserSO search)
        {
            HIS_IMP_MEST_TYPE_USER result = null;
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
