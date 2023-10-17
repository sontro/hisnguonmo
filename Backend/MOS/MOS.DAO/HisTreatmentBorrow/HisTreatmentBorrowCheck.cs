using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisTreatmentBorrow
{
    partial class HisTreatmentBorrowCheck : EntityBase
    {
        public HisTreatmentBorrowCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TREATMENT_BORROW>();
        }

        private BridgeDAO<HIS_TREATMENT_BORROW> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
