using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisServiceReqMety
{
    partial class HisServiceReqMetyCheck : EntityBase
    {
        public HisServiceReqMetyCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_REQ_METY>();
        }

        private BridgeDAO<HIS_SERVICE_REQ_METY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
