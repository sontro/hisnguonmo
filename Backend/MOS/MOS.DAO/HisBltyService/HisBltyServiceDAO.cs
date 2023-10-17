using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBltyService
{
    public partial class HisBltyServiceDAO : EntityBase
    {
        private HisBltyServiceCreate CreateWorker
        {
            get
            {
                return (HisBltyServiceCreate)Worker.Get<HisBltyServiceCreate>();
            }
        }
        private HisBltyServiceUpdate UpdateWorker
        {
            get
            {
                return (HisBltyServiceUpdate)Worker.Get<HisBltyServiceUpdate>();
            }
        }
        private HisBltyServiceDelete DeleteWorker
        {
            get
            {
                return (HisBltyServiceDelete)Worker.Get<HisBltyServiceDelete>();
            }
        }
        private HisBltyServiceTruncate TruncateWorker
        {
            get
            {
                return (HisBltyServiceTruncate)Worker.Get<HisBltyServiceTruncate>();
            }
        }
        private HisBltyServiceGet GetWorker
        {
            get
            {
                return (HisBltyServiceGet)Worker.Get<HisBltyServiceGet>();
            }
        }
        private HisBltyServiceCheck CheckWorker
        {
            get
            {
                return (HisBltyServiceCheck)Worker.Get<HisBltyServiceCheck>();
            }
        }

        public bool Create(HIS_BLTY_SERVICE data)
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

        public bool CreateList(List<HIS_BLTY_SERVICE> listData)
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

        public bool Update(HIS_BLTY_SERVICE data)
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

        public bool UpdateList(List<HIS_BLTY_SERVICE> listData)
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

        public bool Delete(HIS_BLTY_SERVICE data)
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

        public bool DeleteList(List<HIS_BLTY_SERVICE> listData)
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

        public bool Truncate(HIS_BLTY_SERVICE data)
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

        public bool TruncateList(List<HIS_BLTY_SERVICE> listData)
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

        public List<HIS_BLTY_SERVICE> Get(HisBltyServiceSO search, CommonParam param)
        {
            List<HIS_BLTY_SERVICE> result = new List<HIS_BLTY_SERVICE>();
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

        public HIS_BLTY_SERVICE GetById(long id, HisBltyServiceSO search)
        {
            HIS_BLTY_SERVICE result = null;
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
