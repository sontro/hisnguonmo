using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisServiceReqStt
{
    partial class HisServiceReqSttCheck : EntityBase
    {
        public HisServiceReqSttCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_REQ_STT>();
        }

        private BridgeDAO<HIS_SERVICE_REQ_STT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
