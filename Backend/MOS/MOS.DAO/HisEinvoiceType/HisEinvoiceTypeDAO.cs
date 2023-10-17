using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisEinvoiceType
{
    public partial class HisEinvoiceTypeDAO : EntityBase
    {
        private HisEinvoiceTypeCreate CreateWorker
        {
            get
            {
                return (HisEinvoiceTypeCreate)Worker.Get<HisEinvoiceTypeCreate>();
            }
        }
        private HisEinvoiceTypeUpdate UpdateWorker
        {
            get
            {
                return (HisEinvoiceTypeUpdate)Worker.Get<HisEinvoiceTypeUpdate>();
            }
        }
        private HisEinvoiceTypeDelete DeleteWorker
        {
            get
            {
                return (HisEinvoiceTypeDelete)Worker.Get<HisEinvoiceTypeDelete>();
            }
        }
        private HisEinvoiceTypeTruncate TruncateWorker
        {
            get
            {
                return (HisEinvoiceTypeTruncate)Worker.Get<HisEinvoiceTypeTruncate>();
            }
        }
        private HisEinvoiceTypeGet GetWorker
        {
            get
            {
                return (HisEinvoiceTypeGet)Worker.Get<HisEinvoiceTypeGet>();
            }
        }
        private HisEinvoiceTypeCheck CheckWorker
        {
            get
            {
                return (HisEinvoiceTypeCheck)Worker.Get<HisEinvoiceTypeCheck>();
            }
        }

        public bool Create(HIS_EINVOICE_TYPE data)
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

        public bool CreateList(List<HIS_EINVOICE_TYPE> listData)
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

        public bool Update(HIS_EINVOICE_TYPE data)
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

        public bool UpdateList(List<HIS_EINVOICE_TYPE> listData)
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

        public bool Delete(HIS_EINVOICE_TYPE data)
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

        public bool DeleteList(List<HIS_EINVOICE_TYPE> listData)
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

        public bool Truncate(HIS_EINVOICE_TYPE data)
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

        public bool TruncateList(List<HIS_EINVOICE_TYPE> listData)
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

        public List<HIS_EINVOICE_TYPE> Get(HisEinvoiceTypeSO search, CommonParam param)
        {
            List<HIS_EINVOICE_TYPE> result = new List<HIS_EINVOICE_TYPE>();
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

        public HIS_EINVOICE_TYPE GetById(long id, HisEinvoiceTypeSO search)
        {
            HIS_EINVOICE_TYPE result = null;
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
