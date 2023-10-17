using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServiceReqStt
{
    partial class HisServiceReqSttTruncate : EntityBase
    {
        public HisServiceReqSttTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_REQ_STT>();
        }

        private BridgeDAO<HIS_SERVICE_REQ_STT> bridgeDAO;

        public bool Truncate(HIS_SERVICE_REQ_STT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_SERVICE_REQ_STT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
