using Inventec.Common.Repository;
using Inventec.Core;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAwareness
{
    public partial class HisAwarenessDAO : EntityBase
    {
        private HisAwarenessCreate CreateWorker
        {
            get
            {
                return (HisAwarenessCreate)Worker.Get<HisAwarenessCreate>();
            }
        }
        private HisAwarenessUpdate UpdateWorker
        {
            get
            {
                return (HisAwarenessUpdate)Worker.Get<HisAwarenessUpdate>();
            }
        }
        private HisAwarenessDelete DeleteWorker
        {
            get
            {
                return (HisAwarenessDelete)Worker.Get<HisAwarenessDelete>();
            }
        }
        private HisAwarenessTruncate TruncateWorker
        {
            get
            {
                return (HisAwarenessTruncate)Worker.Get<HisAwarenessTruncate>();
            }
        }
        private HisAwarenessGet GetWorker
        {
            get
            {
                return (HisAwarenessGet)Worker.Get<HisAwarenessGet>();
            }
        }
        private HisAwarenessCheck CheckWorker
        {
            get
            {
                return (HisAwarenessCheck)Worker.Get<HisAwarenessCheck>();
            }
        }

        public bool Create(HIS_AWARENESS data)
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

        public bool CreateList(List<HIS_AWARENESS> listData)
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

        public bool Update(HIS_AWARENESS data)
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

        public bool UpdateList(List<HIS_AWARENESS> listData)
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

        public bool Delete(HIS_AWARENESS data)
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

        public bool DeleteList(List<HIS_AWARENESS> listData)
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

        public bool Truncate(HIS_AWARENESS data)
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

        public bool TruncateList(List<HIS_AWARENESS> listData)
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

        public List<HIS_AWARENESS> Get(HisAwarenessSO search, CommonParam param)
        {
            List<HIS_AWARENESS> result = new List<HIS_AWARENESS>();
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

        public HIS_AWARENESS GetById(long id, HisAwarenessSO search)
        {
            HIS_AWARENESS result = null;
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
