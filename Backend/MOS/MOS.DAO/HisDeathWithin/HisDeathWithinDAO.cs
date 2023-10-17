using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisDeathWithin
{
    public partial class HisDeathWithinDAO : EntityBase
    {
        private HisDeathWithinCreate CreateWorker
        {
            get
            {
                return (HisDeathWithinCreate)Worker.Get<HisDeathWithinCreate>();
            }
        }
        private HisDeathWithinUpdate UpdateWorker
        {
            get
            {
                return (HisDeathWithinUpdate)Worker.Get<HisDeathWithinUpdate>();
            }
        }
        private HisDeathWithinDelete DeleteWorker
        {
            get
            {
                return (HisDeathWithinDelete)Worker.Get<HisDeathWithinDelete>();
            }
        }
        private HisDeathWithinTruncate TruncateWorker
        {
            get
            {
                return (HisDeathWithinTruncate)Worker.Get<HisDeathWithinTruncate>();
            }
        }
        private HisDeathWithinGet GetWorker
        {
            get
            {
                return (HisDeathWithinGet)Worker.Get<HisDeathWithinGet>();
            }
        }
        private HisDeathWithinCheck CheckWorker
        {
            get
            {
                return (HisDeathWithinCheck)Worker.Get<HisDeathWithinCheck>();
            }
        }

        public bool Create(HIS_DEATH_WITHIN data)
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

        public bool CreateList(List<HIS_DEATH_WITHIN> listData)
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

        public bool Update(HIS_DEATH_WITHIN data)
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

        public bool UpdateList(List<HIS_DEATH_WITHIN> listData)
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

        public bool Delete(HIS_DEATH_WITHIN data)
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

        public bool DeleteList(List<HIS_DEATH_WITHIN> listData)
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

        public bool Truncate(HIS_DEATH_WITHIN data)
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

        public bool TruncateList(List<HIS_DEATH_WITHIN> listData)
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

        public List<HIS_DEATH_WITHIN> Get(HisDeathWithinSO search, CommonParam param)
        {
            List<HIS_DEATH_WITHIN> result = new List<HIS_DEATH_WITHIN>();
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

        public HIS_DEATH_WITHIN GetById(long id, HisDeathWithinSO search)
        {
            HIS_DEATH_WITHIN result = null;
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
