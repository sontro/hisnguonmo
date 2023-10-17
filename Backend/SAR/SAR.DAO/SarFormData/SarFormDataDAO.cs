using SAR.DAO.StagingObject;
using SAR.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.DAO.SarFormData
{
    public partial class SarFormDataDAO : EntityBase
    {
        private SarFormDataCreate CreateWorker
        {
            get
            {
                return (SarFormDataCreate)Worker.Get<SarFormDataCreate>();
            }
        }
        private SarFormDataUpdate UpdateWorker
        {
            get
            {
                return (SarFormDataUpdate)Worker.Get<SarFormDataUpdate>();
            }
        }
        private SarFormDataDelete DeleteWorker
        {
            get
            {
                return (SarFormDataDelete)Worker.Get<SarFormDataDelete>();
            }
        }
        private SarFormDataTruncate TruncateWorker
        {
            get
            {
                return (SarFormDataTruncate)Worker.Get<SarFormDataTruncate>();
            }
        }
        private SarFormDataGet GetWorker
        {
            get
            {
                return (SarFormDataGet)Worker.Get<SarFormDataGet>();
            }
        }
        private SarFormDataCheck CheckWorker
        {
            get
            {
                return (SarFormDataCheck)Worker.Get<SarFormDataCheck>();
            }
        }

        public bool Create(SAR_FORM_DATA data)
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

        public bool CreateList(List<SAR_FORM_DATA> listData)
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

        public bool Update(SAR_FORM_DATA data)
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

        public bool UpdateList(List<SAR_FORM_DATA> listData)
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

        public bool Delete(SAR_FORM_DATA data)
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

        public bool DeleteList(List<SAR_FORM_DATA> listData)
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

        public bool Truncate(SAR_FORM_DATA data)
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

        public bool TruncateList(List<SAR_FORM_DATA> listData)
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

        public List<SAR_FORM_DATA> Get(SarFormDataSO search, CommonParam param)
        {
            List<SAR_FORM_DATA> result = new List<SAR_FORM_DATA>();
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

        public SAR_FORM_DATA GetById(long id, SarFormDataSO search)
        {
            SAR_FORM_DATA result = null;
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
