using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediRecordBorrow
{
    partial class HisMediRecordBorrowGet : BusinessBase
    {
        internal HIS_MEDI_RECORD_BORROW GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisMediRecordBorrowFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEDI_RECORD_BORROW GetByCode(string code, HisMediRecordBorrowFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMediRecordBorrowDAO.GetByCode(code, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
