using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisUserInvoiceBook
{
    public partial class HisUserInvoiceBookDAO : EntityBase
    {
        private HisUserInvoiceBookCreate CreateWorker
        {
            get
            {
                return (HisUserInvoiceBookCreate)Worker.Get<HisUserInvoiceBookCreate>();
            }
        }
        private HisUserInvoiceBookUpdate UpdateWorker
        {
            get
            {
                return (HisUserInvoiceBookUpdate)Worker.Get<HisUserInvoiceBookUpdate>();
            }
        }
        private HisUserInvoiceBookDelete DeleteWorker
        {
            get
            {
                return (HisUserInvoiceBookDelete)Worker.Get<HisUserInvoiceBookDelete>();
            }
        }
        private HisUserInvoiceBookTruncate TruncateWorker
        {
            get
            {
                return (HisUserInvoiceBookTruncate)Worker.Get<HisUserInvoiceBookTruncate>();
            }
        }
        private HisUserInvoiceBookGet GetWorker
        {
            get
            {
                return (HisUserInvoiceBookGet)Worker.Get<HisUserInvoiceBookGet>();
            }
        }
        private HisUserInvoiceBookCheck CheckWorker
        {
            get
            {
                return (HisUserInvoiceBookCheck)Worker.Get<HisUserInvoiceBookCheck>();
            }
        }

        public bool Create(HIS_USER_INVOICE_BOOK data)
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

        public bool CreateList(List<HIS_USER_INVOICE_BOOK> listData)
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

        public bool Update(HIS_USER_INVOICE_BOOK data)
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

        public bool UpdateList(List<HIS_USER_INVOICE_BOOK> listData)
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

        public bool Delete(HIS_USER_INVOICE_BOOK data)
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

        public bool DeleteList(List<HIS_USER_INVOICE_BOOK> listData)
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

        public bool Truncate(HIS_USER_INVOICE_BOOK data)
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

        public bool TruncateList(List<HIS_USER_INVOICE_BOOK> listData)
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

        public List<HIS_USER_INVOICE_BOOK> Get(HisUserInvoiceBookSO search, CommonParam param)
        {
            List<HIS_USER_INVOICE_BOOK> result = new List<HIS_USER_INVOICE_BOOK>();
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

        public HIS_USER_INVOICE_BOOK GetById(long id, HisUserInvoiceBookSO search)
        {
            HIS_USER_INVOICE_BOOK result = null;
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
