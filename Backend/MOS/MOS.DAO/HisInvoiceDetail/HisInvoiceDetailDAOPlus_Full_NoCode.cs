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
    }
}
