using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSeseTransReq
{
    partial class HisSeseTransReqTruncate : EntityBase
    {
        public HisSeseTransReqTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SESE_TRANS_REQ>();
        }

        private BridgeDAO<HIS_SESE_TRANS_REQ> bridgeDAO;

        public bool Truncate(HIS_SESE_TRANS_REQ data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_SESE_TRANS_REQ> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
