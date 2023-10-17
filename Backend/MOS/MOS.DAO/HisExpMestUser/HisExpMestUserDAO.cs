using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisExpMestUser
{
    public partial class HisExpMestUserDAO : EntityBase
    {
        private HisExpMestUserCreate CreateWorker
        {
            get
            {
                return (HisExpMestUserCreate)Worker.Get<HisExpMestUserCreate>();
            }
        }
        private HisExpMestUserUpdate UpdateWorker
        {
            get
            {
                return (HisExpMestUserUpdate)Worker.Get<HisExpMestUserUpdate>();
            }
        }
        private HisExpMestUserDelete DeleteWorker
        {
            get
            {
                return (HisExpMestUserDelete)Worker.Get<HisExpMestUserDelete>();
            }
        }
        private HisExpMestUserTruncate TruncateWorker
        {
            get
            {
                return (HisExpMestUserTruncate)Worker.Get<HisExpMestUserTruncate>();
            }
        }
        private HisExpMestUserGet GetWorker
        {
            get
            {
                return (HisExpMestUserGet)Worker.Get<HisExpMestUserGet>();
            }
        }
        private HisExpMestUserCheck CheckWorker
        {
            get
            {
                return (HisExpMestUserCheck)Worker.Get<HisExpMestUserCheck>();
            }
        }

        public bool Create(HIS_EXP_MEST_USER data)
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

        public bool CreateList(List<HIS_EXP_MEST_USER> listData)
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

        public bool Update(HIS_EXP_MEST_USER data)
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

        public bool UpdateList(List<HIS_EXP_MEST_USER> listData)
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

        public bool Delete(HIS_EXP_MEST_USER data)
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

        public bool DeleteList(List<HIS_EXP_MEST_USER> listData)
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

        public bool Truncate(HIS_EXP_MEST_USER data)
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

        public bool TruncateList(List<HIS_EXP_MEST_USER> listData)
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

        public List<HIS_EXP_MEST_USER> Get(HisExpMestUserSO search, CommonParam param)
        {
            List<HIS_EXP_MEST_USER> result = new List<HIS_EXP_MEST_USER>();
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

        public HIS_EXP_MEST_USER GetById(long id, HisExpMestUserSO search)
        {
            HIS_EXP_MEST_USER result = null;
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
