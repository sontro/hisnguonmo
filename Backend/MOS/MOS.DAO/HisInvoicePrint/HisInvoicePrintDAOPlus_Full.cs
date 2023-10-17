using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisInvoicePrint
{
    public partial class HisInvoicePrintDAO : EntityBase
    {
        public List<V_HIS_INVOICE_PRINT> GetView(HisInvoicePrintSO search, CommonParam param)
        {
            List<V_HIS_INVOICE_PRINT> result = new List<V_HIS_INVOICE_PRINT>();

            try
            {
                result = GetWorker.GetView(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }

            return result;
        }

        public HIS_INVOICE_PRINT GetByCode(string code, HisInvoicePrintSO search)
        {
            HIS_INVOICE_PRINT result = null;

            try
            {
                result = GetWorker.GetByCode(code, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }
        
        public V_HIS_INVOICE_PRINT GetViewById(long id, HisInvoicePrintSO search)
        {
            V_HIS_INVOICE_PRINT result = null;

            try
            {
                result = GetWorker.GetViewById(id, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }

        public V_HIS_INVOICE_PRINT GetViewByCode(string code, HisInvoicePrintSO search)
        {
            V_HIS_INVOICE_PRINT result = null;

            try
            {
                result = GetWorker.GetViewByCode(code, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        public Dictionary<string, HIS_INVOICE_PRINT> GetDicByCode(HisInvoicePrintSO search, CommonParam param)
        {
            Dictionary<string, HIS_INVOICE_PRINT> result = new Dictionary<string, HIS_INVOICE_PRINT>();
            try
            {
                result = GetWorker.GetDicByCode(search, param);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }

            return result;
        }

        public bool ExistsCode(string code, long? id)
        {
            try
            {
                return CheckWorker.ExistsCode(code, id);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                throw;
            }
        }
    }
}
