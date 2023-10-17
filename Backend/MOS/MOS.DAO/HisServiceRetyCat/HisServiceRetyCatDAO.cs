using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceRetyCat
{
    public partial class HisServiceRetyCatDAO : EntityBase
    {
        private HisServiceRetyCatCreate CreateWorker
        {
            get
            {
                return (HisServiceRetyCatCreate)Worker.Get<HisServiceRetyCatCreate>();
            }
        }
        private HisServiceRetyCatUpdate UpdateWorker
        {
            get
            {
                return (HisServiceRetyCatUpdate)Worker.Get<HisServiceRetyCatUpdate>();
            }
        }
        private HisServiceRetyCatDelete DeleteWorker
        {
            get
            {
                return (HisServiceRetyCatDelete)Worker.Get<HisServiceRetyCatDelete>();
            }
        }
        private HisServiceRetyCatTruncate TruncateWorker
        {
            get
            {
                return (HisServiceRetyCatTruncate)Worker.Get<HisServiceRetyCatTruncate>();
            }
        }
        private HisServiceRetyCatGet GetWorker
        {
            get
            {
                return (HisServiceRetyCatGet)Worker.Get<HisServiceRetyCatGet>();
            }
        }
        private HisServiceRetyCatCheck CheckWorker
        {
            get
            {
                return (HisServiceRetyCatCheck)Worker.Get<HisServiceRetyCatCheck>();
            }
        }

        public bool Create(HIS_SERVICE_RETY_CAT data)
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

        public bool CreateList(List<HIS_SERVICE_RETY_CAT> listData)
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

        public bool Update(HIS_SERVICE_RETY_CAT data)
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

        public bool UpdateList(List<HIS_SERVICE_RETY_CAT> listData)
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

        public bool Delete(HIS_SERVICE_RETY_CAT data)
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

        public bool DeleteList(List<HIS_SERVICE_RETY_CAT> listData)
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

        public bool Truncate(HIS_SERVICE_RETY_CAT data)
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

        public bool TruncateList(List<HIS_SERVICE_RETY_CAT> listData)
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

        public List<HIS_SERVICE_RETY_CAT> Get(HisServiceRetyCatSO search, CommonParam param)
        {
            List<HIS_SERVICE_RETY_CAT> result = new List<HIS_SERVICE_RETY_CAT>();
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

        public HIS_SERVICE_RETY_CAT GetById(long id, HisServiceRetyCatSO search)
        {
            HIS_SERVICE_RETY_CAT result = null;
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
