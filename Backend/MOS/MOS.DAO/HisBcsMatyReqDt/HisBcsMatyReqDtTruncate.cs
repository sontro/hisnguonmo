using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBcsMatyReqDt
{
    partial class HisBcsMatyReqDtTruncate : EntityBase
    {
        public HisBcsMatyReqDtTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BCS_MATY_REQ_DT>();
        }

        private BridgeDAO<HIS_BCS_MATY_REQ_DT> bridgeDAO;

        public bool Truncate(HIS_BCS_MATY_REQ_DT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_BCS_MATY_REQ_DT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
