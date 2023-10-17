using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisInvoicePrint
{
    partial class HisInvoicePrintGet : EntityBase
    {
        public HIS_INVOICE_PRINT GetByCode(string code, HisInvoicePrintSO search)
        {
            HIS_INVOICE_PRINT result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_INVOICE_PRINT.AsQueryable().Where(p => p.INVOICE_PRINT_CODE == code);
                        if (search.listHisInvoicePrintExpression != null && search.listHisInvoicePrintExpression.Count > 0)
                        {
                            foreach (var item in search.listHisInvoicePrintExpression)
                            {
                                query = query.Where(item);
                            }
                        }
                        result = query.SingleOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                Logging(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => code), code) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => search), search), LogType.Error);
                LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
