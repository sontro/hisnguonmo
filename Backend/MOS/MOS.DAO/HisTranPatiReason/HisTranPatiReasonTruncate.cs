using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTranPatiReason
{
    partial class HisTranPatiReasonTruncate : EntityBase
    {
        public HisTranPatiReasonTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRAN_PATI_REASON>();
        }

        private BridgeDAO<HIS_TRAN_PATI_REASON> bridgeDAO;

        public bool Truncate(HIS_TRAN_PATI_REASON data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_TRAN_PATI_REASON> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
