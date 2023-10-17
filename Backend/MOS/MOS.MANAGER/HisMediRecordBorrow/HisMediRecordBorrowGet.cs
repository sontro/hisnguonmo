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
        internal HisMediRecordBorrowGet()
            : base()
        {

        }

        internal HisMediRecordBorrowGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_MEDI_RECORD_BORROW> Get(HisMediRecordBorrowFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMediRecordBorrowDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEDI_RECORD_BORROW GetById(long id)
        {
            try
            {
                return GetById(id, new HisMediRecordBorrowFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEDI_RECORD_BORROW GetById(long id, HisMediRecordBorrowFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMediRecordBorrowDAO.GetById(id, filter.Query());
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
