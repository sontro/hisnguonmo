using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisDepositReq
{
    partial class HisDepositReqCheck : EntityBase
    {
        public HisDepositReqCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEPOSIT_REQ>();
        }

        private BridgeDAO<HIS_DEPOSIT_REQ> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
