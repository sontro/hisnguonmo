using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServiceReqMety
{
    partial class HisServiceReqMetyTruncate : EntityBase
    {
        public HisServiceReqMetyTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_REQ_METY>();
        }

        private BridgeDAO<HIS_SERVICE_REQ_METY> bridgeDAO;

        public bool Truncate(HIS_SERVICE_REQ_METY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_SERVICE_REQ_METY> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
