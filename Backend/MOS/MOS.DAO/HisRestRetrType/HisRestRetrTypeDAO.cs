using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisRestRetrType
{
    public partial class HisRestRetrTypeDAO : EntityBase
    {
        private HisRestRetrTypeCreate CreateWorker
        {
            get
            {
                return (HisRestRetrTypeCreate)Worker.Get<HisRestRetrTypeCreate>();
            }
        }
        private HisRestRetrTypeUpdate UpdateWorker
        {
            get
            {
                return (HisRestRetrTypeUpdate)Worker.Get<HisRestRetrTypeUpdate>();
            }
        }
        private HisRestRetrTypeDelete DeleteWorker
        {
            get
            {
                return (HisRestRetrTypeDelete)Worker.Get<HisRestRetrTypeDelete>();
            }
        }
        private HisRestRetrTypeTruncate TruncateWorker
        {
            get
            {
                return (HisRestRetrTypeTruncate)Worker.Get<HisRestRetrTypeTruncate>();
            }
        }
        private HisRestRetrTypeGet GetWorker
        {
            get
            {
                return (HisRestRetrTypeGet)Worker.Get<HisRestRetrTypeGet>();
            }
        }
        private HisRestRetrTypeCheck CheckWorker
        {
            get
            {
                return (HisRestRetrTypeCheck)Worker.Get<HisRestRetrTypeCheck>();
            }
        }

        public bool Create(HIS_REST_RETR_TYPE data)
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

        public bool CreateList(List<HIS_REST_RETR_TYPE> listData)
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

        public bool Update(HIS_REST_RETR_TYPE data)
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

        public bool UpdateList(List<HIS_REST_RETR_TYPE> listData)
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

        public bool Delete(HIS_REST_RETR_TYPE data)
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

        public bool DeleteList(List<HIS_REST_RETR_TYPE> listData)
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

        public bool Truncate(HIS_REST_RETR_TYPE data)
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

        public bool TruncateList(List<HIS_REST_RETR_TYPE> listData)
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

        public List<HIS_REST_RETR_TYPE> Get(HisRestRetrTypeSO search, CommonParam param)
        {
            List<HIS_REST_RETR_TYPE> result = new List<HIS_REST_RETR_TYPE>();
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

        public HIS_REST_RETR_TYPE GetById(long id, HisRestRetrTypeSO search)
        {
            HIS_REST_RETR_TYPE result = null;
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
