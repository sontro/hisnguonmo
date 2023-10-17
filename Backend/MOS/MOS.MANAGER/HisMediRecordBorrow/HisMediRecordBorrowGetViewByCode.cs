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
        internal V_HIS_MEDI_RECORD_BORROW GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisMediRecordBorrowViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_MEDI_RECORD_BORROW GetViewByCode(string code, HisMediRecordBorrowViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMediRecordBorrowDAO.GetViewByCode(code, filter.Query());
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
