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
        internal List<V_HIS_MEDI_RECORD_BORROW> GetView(HisMediRecordBorrowViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMediRecordBorrowDAO.GetView(filter.Query(), param);
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
