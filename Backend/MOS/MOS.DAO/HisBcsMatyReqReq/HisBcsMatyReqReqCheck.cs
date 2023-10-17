using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisBcsMatyReqReq
{
    partial class HisBcsMatyReqReqCheck : EntityBase
    {
        public HisBcsMatyReqReqCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BCS_MATY_REQ_REQ>();
        }

        private BridgeDAO<HIS_BCS_MATY_REQ_REQ> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
