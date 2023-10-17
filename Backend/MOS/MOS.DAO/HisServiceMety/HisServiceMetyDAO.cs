using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceMety
{
    public partial class HisServiceMetyDAO : EntityBase
    {
        private HisServiceMetyCreate CreateWorker
        {
            get
            {
                return (HisServiceMetyCreate)Worker.Get<HisServiceMetyCreate>();
            }
        }
        private HisServiceMetyUpdate UpdateWorker
        {
            get
            {
                return (HisServiceMetyUpdate)Worker.Get<HisServiceMetyUpdate>();
            }
        }
        private HisServiceMetyDelete DeleteWorker
        {
            get
            {
                return (HisServiceMetyDelete)Worker.Get<HisServiceMetyDelete>();
            }
        }
        private HisServiceMetyTruncate TruncateWorker
        {
            get
            {
                return (HisServiceMetyTruncate)Worker.Get<HisServiceMetyTruncate>();
            }
        }
        private HisServiceMetyGet GetWorker
        {
            get
            {
                return (HisServiceMetyGet)Worker.Get<HisServiceMetyGet>();
            }
        }
        private HisServiceMetyCheck CheckWorker
        {
            get
            {
                return (HisServiceMetyCheck)Worker.Get<HisServiceMetyCheck>();
            }
        }

        public bool Create(HIS_SERVICE_METY data)
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

        public bool CreateList(List<HIS_SERVICE_METY> listData)
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

        public bool Update(HIS_SERVICE_METY data)
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

        public bool UpdateList(List<HIS_SERVICE_METY> listData)
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

        public bool Delete(HIS_SERVICE_METY data)
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

        public bool DeleteList(List<HIS_SERVICE_METY> listData)
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

        public bool Truncate(HIS_SERVICE_METY data)
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

        public bool TruncateList(List<HIS_SERVICE_METY> listData)
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

        public List<HIS_SERVICE_METY> Get(HisServiceMetySO search, CommonParam param)
        {
            List<HIS_SERVICE_METY> result = new List<HIS_SERVICE_METY>();
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

        public HIS_SERVICE_METY GetById(long id, HisServiceMetySO search)
        {
            HIS_SERVICE_METY result = null;
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
