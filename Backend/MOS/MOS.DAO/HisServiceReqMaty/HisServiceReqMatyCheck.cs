using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisServiceReqMaty
{
    partial class HisServiceReqMatyCheck : EntityBase
    {
        public HisServiceReqMatyCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_REQ_MATY>();
        }

        private BridgeDAO<HIS_SERVICE_REQ_MATY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
