using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisHeinApproval
{
    partial class HisHeinApprovalTruncate : EntityBase
    {
        public HisHeinApprovalTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_HEIN_APPROVAL>();
        }

        private BridgeDAO<HIS_HEIN_APPROVAL> bridgeDAO;

        public bool Truncate(HIS_HEIN_APPROVAL data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_HEIN_APPROVAL> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
