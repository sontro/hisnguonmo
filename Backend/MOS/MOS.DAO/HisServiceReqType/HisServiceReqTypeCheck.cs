using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisServiceReqType
{
    partial class HisServiceReqTypeCheck : EntityBase
    {
        public HisServiceReqTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_REQ_TYPE>();
        }

        private BridgeDAO<HIS_SERVICE_REQ_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
