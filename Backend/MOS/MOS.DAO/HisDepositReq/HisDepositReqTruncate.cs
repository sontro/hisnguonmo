using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDepositReq
{
    partial class HisDepositReqTruncate : EntityBase
    {
        public HisDepositReqTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEPOSIT_REQ>();
        }

        private BridgeDAO<HIS_DEPOSIT_REQ> bridgeDAO;

        public bool Truncate(HIS_DEPOSIT_REQ data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_DEPOSIT_REQ> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
