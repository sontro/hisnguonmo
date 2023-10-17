using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTreatmentBorrow
{
    partial class HisTreatmentBorrowTruncate : EntityBase
    {
        public HisTreatmentBorrowTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TREATMENT_BORROW>();
        }

        private BridgeDAO<HIS_TREATMENT_BORROW> bridgeDAO;

        public bool Truncate(HIS_TREATMENT_BORROW data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_TREATMENT_BORROW> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
