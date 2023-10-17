using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTreatmentBorrow
{
    partial class HisTreatmentBorrowUpdate : EntityBase
    {
        public HisTreatmentBorrowUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TREATMENT_BORROW>();
        }

        private BridgeDAO<HIS_TREATMENT_BORROW> bridgeDAO;

        public bool Update(HIS_TREATMENT_BORROW data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_TREATMENT_BORROW> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
