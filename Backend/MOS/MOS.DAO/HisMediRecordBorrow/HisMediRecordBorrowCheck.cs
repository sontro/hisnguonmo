using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMediRecordBorrow
{
    partial class HisMediRecordBorrowCheck : EntityBase
    {
        public HisMediRecordBorrowCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_RECORD_BORROW>();
        }

        private BridgeDAO<HIS_MEDI_RECORD_BORROW> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
