using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisServiceHein
{
    partial class HisServiceHeinCheck : EntityBase
    {
        public HisServiceHeinCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_HEIN>();
        }

        private BridgeDAO<HIS_SERVICE_HEIN> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
