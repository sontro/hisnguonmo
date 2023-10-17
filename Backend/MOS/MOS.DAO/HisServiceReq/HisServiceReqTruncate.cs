using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServiceReq
{
    partial class HisServiceReqTruncate : EntityBase
    {
        public HisServiceReqTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_REQ>();
        }

        private BridgeDAO<HIS_SERVICE_REQ> bridgeDAO;

        public bool Truncate(HIS_SERVICE_REQ data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_SERVICE_REQ> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
