using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMetyMety
{
    public partial class HisMetyMetyDAO : EntityBase
    {
        private HisMetyMetyCreate CreateWorker
        {
            get
            {
                return (HisMetyMetyCreate)Worker.Get<HisMetyMetyCreate>();
            }
        }
        private HisMetyMetyUpdate UpdateWorker
        {
            get
            {
                return (HisMetyMetyUpdate)Worker.Get<HisMetyMetyUpdate>();
            }
        }
        private HisMetyMetyDelete DeleteWorker
        {
            get
            {
                return (HisMetyMetyDelete)Worker.Get<HisMetyMetyDelete>();
            }
        }
        private HisMetyMetyTruncate TruncateWorker
        {
            get
            {
                return (HisMetyMetyTruncate)Worker.Get<HisMetyMetyTruncate>();
            }
        }
        private HisMetyMetyGet GetWorker
        {
            get
            {
                return (HisMetyMetyGet)Worker.Get<HisMetyMetyGet>();
            }
        }
        private HisMetyMetyCheck CheckWorker
        {
            get
            {
                return (HisMetyMetyCheck)Worker.Get<HisMetyMetyCheck>();
            }
        }

        public bool Create(HIS_METY_METY data)
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

        public bool CreateList(List<HIS_METY_METY> listData)
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

        public bool Update(HIS_METY_METY data)
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

        public bool UpdateList(List<HIS_METY_METY> listData)
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

        public bool Delete(HIS_METY_METY data)
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

        public bool DeleteList(List<HIS_METY_METY> listData)
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

        public bool Truncate(HIS_METY_METY data)
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

        public bool TruncateList(List<HIS_METY_METY> listData)
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

        public List<HIS_METY_METY> Get(HisMetyMetySO search, CommonParam param)
        {
            List<HIS_METY_METY> result = new List<HIS_METY_METY>();
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

        public HIS_METY_METY GetById(long id, HisMetyMetySO search)
        {
            HIS_METY_METY result = null;
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
