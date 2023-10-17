using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDocumentBook
{
    partial class HisDocumentBookGet : EntityBase
    {
        public V_HIS_DOCUMENT_BOOK GetViewByCode(string code, HisDocumentBookSO search)
        {
            V_HIS_DOCUMENT_BOOK result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_DOCUMENT_BOOK.AsQueryable().Where(p => p.DOCUMENT_BOOK_CODE == code);
                        if (search.listVHisDocumentBookExpression != null && search.listVHisDocumentBookExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisDocumentBookExpression)
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
