using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisManufacturer
{
    public partial class HisManufacturerDAO : EntityBase
    {
        private HisManufacturerCreate CreateWorker
        {
            get
            {
                return (HisManufacturerCreate)Worker.Get<HisManufacturerCreate>();
            }
        }
        private HisManufacturerUpdate UpdateWorker
        {
            get
            {
                return (HisManufacturerUpdate)Worker.Get<HisManufacturerUpdate>();
            }
        }
        private HisManufacturerDelete DeleteWorker
        {
            get
            {
                return (HisManufacturerDelete)Worker.Get<HisManufacturerDelete>();
            }
        }
        private HisManufacturerTruncate TruncateWorker
        {
            get
            {
                return (HisManufacturerTruncate)Worker.Get<HisManufacturerTruncate>();
            }
        }
        private HisManufacturerGet GetWorker
        {
            get
            {
                return (HisManufacturerGet)Worker.Get<HisManufacturerGet>();
            }
        }
        private HisManufacturerCheck CheckWorker
        {
            get
            {
                return (HisManufacturerCheck)Worker.Get<HisManufacturerCheck>();
            }
        }

        public bool Create(HIS_MANUFACTURER data)
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

        public bool CreateList(List<HIS_MANUFACTURER> listData)
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

        public bool Update(HIS_MANUFACTURER data)
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

        public bool UpdateList(List<HIS_MANUFACTURER> listData)
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

        public bool Delete(HIS_MANUFACTURER data)
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

        public bool DeleteList(List<HIS_MANUFACTURER> listData)
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

        public bool Truncate(HIS_MANUFACTURER data)
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

        public bool TruncateList(List<HIS_MANUFACTURER> listData)
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

        public List<HIS_MANUFACTURER> Get(HisManufacturerSO search, CommonParam param)
        {
            List<HIS_MANUFACTURER> result = new List<HIS_MANUFACTURER>();
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

        public HIS_MANUFACTURER GetById(long id, HisManufacturerSO search)
        {
            HIS_MANUFACTURER result = null;
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
