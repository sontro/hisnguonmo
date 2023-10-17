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
        public HIS_MEDI_RECORD_BORROW GetByCode(string code, HisMediRecordBorrowSO search)
        {
            HIS_MEDI_RECORD_BORROW result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_MEDI_RECORD_BORROW.AsQueryable().Where(p => p.MEDI_RECORD_BORROW_CODE == code);
                        if (search.listHisMediRecordBorrowExpression != null && search.listHisMediRecordBorrowExpression.Count > 0)
                        {
                            foreach (var item in search.listHisMediRecordBorrowExpression)
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
