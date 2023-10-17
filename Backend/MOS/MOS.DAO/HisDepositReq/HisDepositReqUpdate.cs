using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDepositReq
{
    partial class HisDepositReqUpdate : EntityBase
    {
        public HisDepositReqUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEPOSIT_REQ>();
        }

        private BridgeDAO<HIS_DEPOSIT_REQ> bridgeDAO;

        public bool Update(HIS_DEPOSIT_REQ data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_DEPOSIT_REQ> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
