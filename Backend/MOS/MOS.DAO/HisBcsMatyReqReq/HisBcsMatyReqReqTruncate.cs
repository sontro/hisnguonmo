using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBcsMatyReqReq
{
    partial class HisBcsMatyReqReqTruncate : EntityBase
    {
        public HisBcsMatyReqReqTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BCS_MATY_REQ_REQ>();
        }

        private BridgeDAO<HIS_BCS_MATY_REQ_REQ> bridgeDAO;

        public bool Truncate(HIS_BCS_MATY_REQ_REQ data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_BCS_MATY_REQ_REQ> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
