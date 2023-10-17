using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServiceReqMaty
{
    partial class HisServiceReqMatyTruncate : EntityBase
    {
        public HisServiceReqMatyTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_REQ_MATY>();
        }

        private BridgeDAO<HIS_SERVICE_REQ_MATY> bridgeDAO;

        public bool Truncate(HIS_SERVICE_REQ_MATY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_SERVICE_REQ_MATY> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
