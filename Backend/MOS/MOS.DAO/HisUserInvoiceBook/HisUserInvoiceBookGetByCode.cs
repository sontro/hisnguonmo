using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisUserInvoiceBook
{
    partial class HisUserInvoiceBookGet : EntityBase
    {
        public HIS_USER_INVOICE_BOOK GetByCode(string code, HisUserInvoiceBookSO search)
        {
            HIS_USER_INVOICE_BOOK result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_USER_INVOICE_BOOK.AsQueryable().Where(p => p.USER_INVOICE_BOOK_CODE == code);
                        if (search.listHisUserInvoiceBookExpression != null && search.listHisUserInvoiceBookExpression.Count > 0)
                        {
                            foreach (var item in search.listHisUserInvoiceBookExpression)
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
