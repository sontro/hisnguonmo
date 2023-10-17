using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisServiceChangeReq
{
    partial class HisServiceChangeReqCheck : EntityBase
    {
        public HisServiceChangeReqCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_CHANGE_REQ>();
        }

        private BridgeDAO<HIS_SERVICE_CHANGE_REQ> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
