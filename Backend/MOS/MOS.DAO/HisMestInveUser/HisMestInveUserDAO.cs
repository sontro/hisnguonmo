using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMestInveUser
{
    public partial class HisMestInveUserDAO : EntityBase
    {
        private HisMestInveUserCreate CreateWorker
        {
            get
            {
                return (HisMestInveUserCreate)Worker.Get<HisMestInveUserCreate>();
            }
        }
        private HisMestInveUserUpdate UpdateWorker
        {
            get
            {
                return (HisMestInveUserUpdate)Worker.Get<HisMestInveUserUpdate>();
            }
        }
        private HisMestInveUserDelete DeleteWorker
        {
            get
            {
                return (HisMestInveUserDelete)Worker.Get<HisMestInveUserDelete>();
            }
        }
        private HisMestInveUserTruncate TruncateWorker
        {
            get
            {
                return (HisMestInveUserTruncate)Worker.Get<HisMestInveUserTruncate>();
            }
        }
        private HisMestInveUserGet GetWorker
        {
            get
            {
                return (HisMestInveUserGet)Worker.Get<HisMestInveUserGet>();
            }
        }
        private HisMestInveUserCheck CheckWorker
        {
            get
            {
                return (HisMestInveUserCheck)Worker.Get<HisMestInveUserCheck>();
            }
        }

        public bool Create(HIS_MEST_INVE_USER data)
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

        public bool CreateList(List<HIS_MEST_INVE_USER> listData)
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

        public bool Update(HIS_MEST_INVE_USER data)
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

        public bool UpdateList(List<HIS_MEST_INVE_USER> listData)
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

        public bool Delete(HIS_MEST_INVE_USER data)
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

        public bool DeleteList(List<HIS_MEST_INVE_USER> listData)
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

        public bool Truncate(HIS_MEST_INVE_USER data)
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

        public bool TruncateList(List<HIS_MEST_INVE_USER> listData)
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

        public List<HIS_MEST_INVE_USER> Get(HisMestInveUserSO search, CommonParam param)
        {
            List<HIS_MEST_INVE_USER> result = new List<HIS_MEST_INVE_USER>();
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

        public HIS_MEST_INVE_USER GetById(long id, HisMestInveUserSO search)
        {
            HIS_MEST_INVE_USER result = null;
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
