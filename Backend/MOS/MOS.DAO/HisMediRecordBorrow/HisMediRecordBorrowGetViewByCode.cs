using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMediRecordBorrow
{
    partial class HisMediRecordBorrowGet : EntityBase
    {
        public V_HIS_MEDI_RECORD_BORROW GetViewByCode(string code, HisMediRecordBorrowSO search)
        {
            V_HIS_MEDI_RECORD_BORROW result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_MEDI_RECORD_BORROW.AsQueryable().Where(p => p.MEDI_RECORD_BORROW_CODE == code);
                        if (search.listVHisMediRecordBorrowExpression != null && search.listVHisMediRecordBorrowExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisMediRecordBorrowExpression)
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
