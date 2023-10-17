using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisServiceRati
{
    partial class HisServiceRatiCheck : EntityBase
    {
        public HisServiceRatiCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_RATI>();
        }

        private BridgeDAO<HIS_SERVICE_RATI> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
