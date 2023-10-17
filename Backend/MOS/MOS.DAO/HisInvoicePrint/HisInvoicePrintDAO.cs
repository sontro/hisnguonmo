using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisInvoicePrint
{
    public partial class HisInvoicePrintDAO : EntityBase
    {
        private HisInvoicePrintCreate CreateWorker
        {
            get
            {
                return (HisInvoicePrintCreate)Worker.Get<HisInvoicePrintCreate>();
            }
        }
        private HisInvoicePrintUpdate UpdateWorker
        {
            get
            {
                return (HisInvoicePrintUpdate)Worker.Get<HisInvoicePrintUpdate>();
            }
        }
        private HisInvoicePrintDelete DeleteWorker
        {
            get
            {
                return (HisInvoicePrintDelete)Worker.Get<HisInvoicePrintDelete>();
            }
        }
        private HisInvoicePrintTruncate TruncateWorker
        {
            get
            {
                return (HisInvoicePrintTruncate)Worker.Get<HisInvoicePrintTruncate>();
            }
        }
        private HisInvoicePrintGet GetWorker
        {
            get
            {
                return (HisInvoicePrintGet)Worker.Get<HisInvoicePrintGet>();
            }
        }
        private HisInvoicePrintCheck CheckWorker
        {
            get
            {
                return (HisInvoicePrintCheck)Worker.Get<HisInvoicePrintCheck>();
            }
        }

        public bool Create(HIS_INVOICE_PRINT data)
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

        public bool CreateList(List<HIS_INVOICE_PRINT> listData)
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

        public bool Update(HIS_INVOICE_PRINT data)
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

        public bool UpdateList(List<HIS_INVOICE_PRINT> listData)
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

        public bool Delete(HIS_INVOICE_PRINT data)
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

        public bool DeleteList(List<HIS_INVOICE_PRINT> listData)
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

        public bool Truncate(HIS_INVOICE_PRINT data)
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

        public bool TruncateList(List<HIS_INVOICE_PRINT> listData)
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

        public List<HIS_INVOICE_PRINT> Get(HisInvoicePrintSO search, CommonParam param)
        {
            List<HIS_INVOICE_PRINT> result = new List<HIS_INVOICE_PRINT>();
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

        public HIS_INVOICE_PRINT GetById(long id, HisInvoicePrintSO search)
        {
            HIS_INVOICE_PRINT result = null;
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
