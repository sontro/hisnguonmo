using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisBcsMetyReqReq
{
    partial class HisBcsMetyReqReqCheck : EntityBase
    {
        public HisBcsMetyReqReqCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BCS_METY_REQ_REQ>();
        }

        private BridgeDAO<HIS_BCS_METY_REQ_REQ> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
