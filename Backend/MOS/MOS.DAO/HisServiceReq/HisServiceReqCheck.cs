using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisServiceReq
{
    partial class HisServiceReqCheck : EntityBase
    {
        public HisServiceReqCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_REQ>();
        }

        private BridgeDAO<HIS_SERVICE_REQ> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
