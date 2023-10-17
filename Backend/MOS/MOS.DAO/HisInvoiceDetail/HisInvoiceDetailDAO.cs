using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisInvoiceDetail
{
    public partial class HisInvoiceDetailDAO : EntityBase
    {
        private HisInvoiceDetailCreate CreateWorker
        {
            get
            {
                return (HisInvoiceDetailCreate)Worker.Get<HisInvoiceDetailCreate>();
            }
        }
        private HisInvoiceDetailUpdate UpdateWorker
        {
            get
            {
                return (HisInvoiceDetailUpdate)Worker.Get<HisInvoiceDetailUpdate>();
            }
        }
        private HisInvoiceDetailDelete DeleteWorker
        {
            get
            {
                return (HisInvoiceDetailDelete)Worker.Get<HisInvoiceDetailDelete>();
            }
        }
        private HisInvoiceDetailTruncate TruncateWorker
        {
            get
            {
                return (HisInvoiceDetailTruncate)Worker.Get<HisInvoiceDetailTruncate>();
            }
        }
        private HisInvoiceDetailGet GetWorker
        {
            get
            {
                return (HisInvoiceDetailGet)Worker.Get<HisInvoiceDetailGet>();
            }
        }
        private HisInvoiceDetailCheck CheckWorker
        {
            get
            {
                return (HisInvoiceDetailCheck)Worker.Get<HisInvoiceDetailCheck>();
            }
        }

        public bool Create(HIS_INVOICE_DETAIL data)
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

        public bool CreateList(List<HIS_INVOICE_DETAIL> listData)
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

        public bool Update(HIS_INVOICE_DETAIL data)
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

        public bool UpdateList(List<HIS_INVOICE_DETAIL> listData)
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

        public bool Delete(HIS_INVOICE_DETAIL data)
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

        public bool DeleteList(List<HIS_INVOICE_DETAIL> listData)
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

        public bool Truncate(HIS_INVOICE_DETAIL data)
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

        public bool TruncateList(List<HIS_INVOICE_DETAIL> listData)
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

        public List<HIS_INVOICE_DETAIL> Get(HisInvoiceDetailSO search, CommonParam param)
        {
            List<HIS_INVOICE_DETAIL> result = new List<HIS_INVOICE_DETAIL>();
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

        public HIS_INVOICE_DETAIL GetById(long id, HisInvoiceDetailSO search)
        {
            HIS_INVOICE_DETAIL result = null;
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
