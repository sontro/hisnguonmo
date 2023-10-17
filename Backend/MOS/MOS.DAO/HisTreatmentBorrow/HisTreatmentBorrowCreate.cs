using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisTreatmentBorrow
{
    partial class HisTreatmentBorrowCreate : EntityBase
    {
        public HisTreatmentBorrowCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TREATMENT_BORROW>();
        }

        private BridgeDAO<HIS_TREATMENT_BORROW> bridgeDAO;

        public bool Create(HIS_TREATMENT_BORROW data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_TREATMENT_BORROW> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
