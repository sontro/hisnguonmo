using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisEmrForm
{
    public partial class HisEmrFormDAO : EntityBase
    {
        private HisEmrFormCreate CreateWorker
        {
            get
            {
                return (HisEmrFormCreate)Worker.Get<HisEmrFormCreate>();
            }
        }
        private HisEmrFormUpdate UpdateWorker
        {
            get
            {
                return (HisEmrFormUpdate)Worker.Get<HisEmrFormUpdate>();
            }
        }
        private HisEmrFormDelete DeleteWorker
        {
            get
            {
                return (HisEmrFormDelete)Worker.Get<HisEmrFormDelete>();
            }
        }
        private HisEmrFormTruncate TruncateWorker
        {
            get
            {
                return (HisEmrFormTruncate)Worker.Get<HisEmrFormTruncate>();
            }
        }
        private HisEmrFormGet GetWorker
        {
            get
            {
                return (HisEmrFormGet)Worker.Get<HisEmrFormGet>();
            }
        }
        private HisEmrFormCheck CheckWorker
        {
            get
            {
                return (HisEmrFormCheck)Worker.Get<HisEmrFormCheck>();
            }
        }

        public bool Create(HIS_EMR_FORM data)
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

        public bool CreateList(List<HIS_EMR_FORM> listData)
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

        public bool Update(HIS_EMR_FORM data)
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

        public bool UpdateList(List<HIS_EMR_FORM> listData)
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

        public bool Delete(HIS_EMR_FORM data)
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

        public bool DeleteList(List<HIS_EMR_FORM> listData)
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

        public bool Truncate(HIS_EMR_FORM data)
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

        public bool TruncateList(List<HIS_EMR_FORM> listData)
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

        public List<HIS_EMR_FORM> Get(HisEmrFormSO search, CommonParam param)
        {
            List<HIS_EMR_FORM> result = new List<HIS_EMR_FORM>();
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

        public HIS_EMR_FORM GetById(long id, HisEmrFormSO search)
        {
            HIS_EMR_FORM result = null;
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
