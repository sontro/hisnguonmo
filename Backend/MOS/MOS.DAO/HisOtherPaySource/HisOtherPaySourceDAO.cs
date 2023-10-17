using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisOtherPaySource
{
    public partial class HisOtherPaySourceDAO : EntityBase
    {
        private HisOtherPaySourceCreate CreateWorker
        {
            get
            {
                return (HisOtherPaySourceCreate)Worker.Get<HisOtherPaySourceCreate>();
            }
        }
        private HisOtherPaySourceUpdate UpdateWorker
        {
            get
            {
                return (HisOtherPaySourceUpdate)Worker.Get<HisOtherPaySourceUpdate>();
            }
        }
        private HisOtherPaySourceDelete DeleteWorker
        {
            get
            {
                return (HisOtherPaySourceDelete)Worker.Get<HisOtherPaySourceDelete>();
            }
        }
        private HisOtherPaySourceTruncate TruncateWorker
        {
            get
            {
                return (HisOtherPaySourceTruncate)Worker.Get<HisOtherPaySourceTruncate>();
            }
        }
        private HisOtherPaySourceGet GetWorker
        {
            get
            {
                return (HisOtherPaySourceGet)Worker.Get<HisOtherPaySourceGet>();
            }
        }
        private HisOtherPaySourceCheck CheckWorker
        {
            get
            {
                return (HisOtherPaySourceCheck)Worker.Get<HisOtherPaySourceCheck>();
            }
        }

        public bool Create(HIS_OTHER_PAY_SOURCE data)
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

        public bool CreateList(List<HIS_OTHER_PAY_SOURCE> listData)
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

        public bool Update(HIS_OTHER_PAY_SOURCE data)
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

        public bool UpdateList(List<HIS_OTHER_PAY_SOURCE> listData)
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

        public bool Delete(HIS_OTHER_PAY_SOURCE data)
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

        public bool DeleteList(List<HIS_OTHER_PAY_SOURCE> listData)
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

        public bool Truncate(HIS_OTHER_PAY_SOURCE data)
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

        public bool TruncateList(List<HIS_OTHER_PAY_SOURCE> listData)
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

        public List<HIS_OTHER_PAY_SOURCE> Get(HisOtherPaySourceSO search, CommonParam param)
        {
            List<HIS_OTHER_PAY_SOURCE> result = new List<HIS_OTHER_PAY_SOURCE>();
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

        public HIS_OTHER_PAY_SOURCE GetById(long id, HisOtherPaySourceSO search)
        {
            HIS_OTHER_PAY_SOURCE result = null;
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
