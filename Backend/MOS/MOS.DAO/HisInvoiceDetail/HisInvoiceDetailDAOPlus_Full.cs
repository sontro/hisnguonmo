using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisInvoiceDetail
{
    public partial class HisInvoiceDetailDAO : EntityBase
    {
        public List<V_HIS_INVOICE_DETAIL> GetView(HisInvoiceDetailSO search, CommonParam param)
        {
            List<V_HIS_INVOICE_DETAIL> result = new List<V_HIS_INVOICE_DETAIL>();

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

        public HIS_INVOICE_DETAIL GetByCode(string code, HisInvoiceDetailSO search)
        {
            HIS_INVOICE_DETAIL result = null;

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
        
        public V_HIS_INVOICE_DETAIL GetViewById(long id, HisInvoiceDetailSO search)
        {
            V_HIS_INVOICE_DETAIL result = null;

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

        public V_HIS_INVOICE_DETAIL GetViewByCode(string code, HisInvoiceDetailSO search)
        {
            V_HIS_INVOICE_DETAIL result = null;

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

        public Dictionary<string, HIS_INVOICE_DETAIL> GetDicByCode(HisInvoiceDetailSO search, CommonParam param)
        {
            Dictionary<string, HIS_INVOICE_DETAIL> result = new Dictionary<string, HIS_INVOICE_DETAIL>();
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
