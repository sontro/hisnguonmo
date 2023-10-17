using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisHeinServiceType
{
    public partial class HisHeinServiceTypeDAO : EntityBase
    {
        private HisHeinServiceTypeCreate CreateWorker
        {
            get
            {
                return (HisHeinServiceTypeCreate)Worker.Get<HisHeinServiceTypeCreate>();
            }
        }
        private HisHeinServiceTypeUpdate UpdateWorker
        {
            get
            {
                return (HisHeinServiceTypeUpdate)Worker.Get<HisHeinServiceTypeUpdate>();
            }
        }
        private HisHeinServiceTypeDelete DeleteWorker
        {
            get
            {
                return (HisHeinServiceTypeDelete)Worker.Get<HisHeinServiceTypeDelete>();
            }
        }
        private HisHeinServiceTypeTruncate TruncateWorker
        {
            get
            {
                return (HisHeinServiceTypeTruncate)Worker.Get<HisHeinServiceTypeTruncate>();
            }
        }
        private HisHeinServiceTypeGet GetWorker
        {
            get
            {
                return (HisHeinServiceTypeGet)Worker.Get<HisHeinServiceTypeGet>();
            }
        }
        private HisHeinServiceTypeCheck CheckWorker
        {
            get
            {
                return (HisHeinServiceTypeCheck)Worker.Get<HisHeinServiceTypeCheck>();
            }
        }

        public bool Create(HIS_HEIN_SERVICE_TYPE data)
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

        public bool CreateList(List<HIS_HEIN_SERVICE_TYPE> listData)
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

        public bool Update(HIS_HEIN_SERVICE_TYPE data)
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

        public bool UpdateList(List<HIS_HEIN_SERVICE_TYPE> listData)
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

        public bool Delete(HIS_HEIN_SERVICE_TYPE data)
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

        public bool DeleteList(List<HIS_HEIN_SERVICE_TYPE> listData)
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

        public bool Truncate(HIS_HEIN_SERVICE_TYPE data)
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

        public bool TruncateList(List<HIS_HEIN_SERVICE_TYPE> listData)
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

        public List<HIS_HEIN_SERVICE_TYPE> Get(HisHeinServiceTypeSO search, CommonParam param)
        {
            List<HIS_HEIN_SERVICE_TYPE> result = new List<HIS_HEIN_SERVICE_TYPE>();
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

        public HIS_HEIN_SERVICE_TYPE GetById(long id, HisHeinServiceTypeSO search)
        {
            HIS_HEIN_SERVICE_TYPE result = null;
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
