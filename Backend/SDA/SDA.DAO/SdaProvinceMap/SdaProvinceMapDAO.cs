using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.DAO.SdaProvinceMap
{
    public partial class SdaProvinceMapDAO : EntityBase
    {
        private SdaProvinceMapCreate CreateWorker
        {
            get
            {
                return (SdaProvinceMapCreate)Worker.Get<SdaProvinceMapCreate>();
            }
        }
        private SdaProvinceMapUpdate UpdateWorker
        {
            get
            {
                return (SdaProvinceMapUpdate)Worker.Get<SdaProvinceMapUpdate>();
            }
        }
        private SdaProvinceMapDelete DeleteWorker
        {
            get
            {
                return (SdaProvinceMapDelete)Worker.Get<SdaProvinceMapDelete>();
            }
        }
        private SdaProvinceMapTruncate TruncateWorker
        {
            get
            {
                return (SdaProvinceMapTruncate)Worker.Get<SdaProvinceMapTruncate>();
            }
        }
        private SdaProvinceMapGet GetWorker
        {
            get
            {
                return (SdaProvinceMapGet)Worker.Get<SdaProvinceMapGet>();
            }
        }
        private SdaProvinceMapCheck CheckWorker
        {
            get
            {
                return (SdaProvinceMapCheck)Worker.Get<SdaProvinceMapCheck>();
            }
        }

        public bool Create(SDA_PROVINCE_MAP data)
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

        public bool CreateList(List<SDA_PROVINCE_MAP> listData)
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

        public bool Update(SDA_PROVINCE_MAP data)
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

        public bool UpdateList(List<SDA_PROVINCE_MAP> listData)
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

        public bool Delete(SDA_PROVINCE_MAP data)
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

        public bool DeleteList(List<SDA_PROVINCE_MAP> listData)
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

        public bool Truncate(SDA_PROVINCE_MAP data)
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

        public bool TruncateList(List<SDA_PROVINCE_MAP> listData)
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

        public List<SDA_PROVINCE_MAP> Get(SdaProvinceMapSO search, CommonParam param)
        {
            List<SDA_PROVINCE_MAP> result = new List<SDA_PROVINCE_MAP>();
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

        public SDA_PROVINCE_MAP GetById(long id, SdaProvinceMapSO search)
        {
            SDA_PROVINCE_MAP result = null;
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
