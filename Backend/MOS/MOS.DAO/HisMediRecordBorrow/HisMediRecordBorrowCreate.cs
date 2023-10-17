using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMediRecordBorrow
{
    partial class HisMediRecordBorrowCreate : EntityBase
    {
        public HisMediRecordBorrowCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDI_RECORD_BORROW>();
        }

        private BridgeDAO<HIS_MEDI_RECORD_BORROW> bridgeDAO;

        public bool Create(HIS_MEDI_RECORD_BORROW data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEDI_RECORD_BORROW> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
