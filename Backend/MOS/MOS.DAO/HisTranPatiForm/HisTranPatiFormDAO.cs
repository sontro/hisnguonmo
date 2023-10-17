using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisTranPatiForm
{
    public partial class HisTranPatiFormDAO : EntityBase
    {
        private HisTranPatiFormCreate CreateWorker
        {
            get
            {
                return (HisTranPatiFormCreate)Worker.Get<HisTranPatiFormCreate>();
            }
        }
        private HisTranPatiFormUpdate UpdateWorker
        {
            get
            {
                return (HisTranPatiFormUpdate)Worker.Get<HisTranPatiFormUpdate>();
            }
        }
        private HisTranPatiFormDelete DeleteWorker
        {
            get
            {
                return (HisTranPatiFormDelete)Worker.Get<HisTranPatiFormDelete>();
            }
        }
        private HisTranPatiFormTruncate TruncateWorker
        {
            get
            {
                return (HisTranPatiFormTruncate)Worker.Get<HisTranPatiFormTruncate>();
            }
        }
        private HisTranPatiFormGet GetWorker
        {
            get
            {
                return (HisTranPatiFormGet)Worker.Get<HisTranPatiFormGet>();
            }
        }
        private HisTranPatiFormCheck CheckWorker
        {
            get
            {
                return (HisTranPatiFormCheck)Worker.Get<HisTranPatiFormCheck>();
            }
        }

        public bool Create(HIS_TRAN_PATI_FORM data)
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

        public bool CreateList(List<HIS_TRAN_PATI_FORM> listData)
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

        public bool Update(HIS_TRAN_PATI_FORM data)
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

        public bool UpdateList(List<HIS_TRAN_PATI_FORM> listData)
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

        public bool Delete(HIS_TRAN_PATI_FORM data)
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

        public bool DeleteList(List<HIS_TRAN_PATI_FORM> listData)
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

        public bool Truncate(HIS_TRAN_PATI_FORM data)
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

        public bool TruncateList(List<HIS_TRAN_PATI_FORM> listData)
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

        public List<HIS_TRAN_PATI_FORM> Get(HisTranPatiFormSO search, CommonParam param)
        {
            List<HIS_TRAN_PATI_FORM> result = new List<HIS_TRAN_PATI_FORM>();
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

        public HIS_TRAN_PATI_FORM GetById(long id, HisTranPatiFormSO search)
        {
            HIS_TRAN_PATI_FORM result = null;
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
