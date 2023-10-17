using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTransReq
{
    partial class HisTransReqTruncate : EntityBase
    {
        public HisTransReqTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRANS_REQ>();
        }

        private BridgeDAO<HIS_TRANS_REQ> bridgeDAO;

        public bool Truncate(HIS_TRANS_REQ data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_TRANS_REQ> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
