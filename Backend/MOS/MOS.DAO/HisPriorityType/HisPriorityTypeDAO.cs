using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisPriorityType
{
    public partial class HisPriorityTypeDAO : EntityBase
    {
        private HisPriorityTypeCreate CreateWorker
        {
            get
            {
                return (HisPriorityTypeCreate)Worker.Get<HisPriorityTypeCreate>();
            }
        }
        private HisPriorityTypeUpdate UpdateWorker
        {
            get
            {
                return (HisPriorityTypeUpdate)Worker.Get<HisPriorityTypeUpdate>();
            }
        }
        private HisPriorityTypeDelete DeleteWorker
        {
            get
            {
                return (HisPriorityTypeDelete)Worker.Get<HisPriorityTypeDelete>();
            }
        }
        private HisPriorityTypeTruncate TruncateWorker
        {
            get
            {
                return (HisPriorityTypeTruncate)Worker.Get<HisPriorityTypeTruncate>();
            }
        }
        private HisPriorityTypeGet GetWorker
        {
            get
            {
                return (HisPriorityTypeGet)Worker.Get<HisPriorityTypeGet>();
            }
        }
        private HisPriorityTypeCheck CheckWorker
        {
            get
            {
                return (HisPriorityTypeCheck)Worker.Get<HisPriorityTypeCheck>();
            }
        }

        public bool Create(HIS_PRIORITY_TYPE data)
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

        public bool CreateList(List<HIS_PRIORITY_TYPE> listData)
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

        public bool Update(HIS_PRIORITY_TYPE data)
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

        public bool UpdateList(List<HIS_PRIORITY_TYPE> listData)
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

        public bool Delete(HIS_PRIORITY_TYPE data)
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

        public bool DeleteList(List<HIS_PRIORITY_TYPE> listData)
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

        public bool Truncate(HIS_PRIORITY_TYPE data)
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

        public bool TruncateList(List<HIS_PRIORITY_TYPE> listData)
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

        public List<HIS_PRIORITY_TYPE> Get(HisPriorityTypeSO search, CommonParam param)
        {
            List<HIS_PRIORITY_TYPE> result = new List<HIS_PRIORITY_TYPE>();
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

        public HIS_PRIORITY_TYPE GetById(long id, HisPriorityTypeSO search)
        {
            HIS_PRIORITY_TYPE result = null;
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
