using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisServiceMaty
{
    partial class HisServiceMatyCheck : EntityBase
    {
        public HisServiceMatyCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_MATY>();
        }

        private BridgeDAO<HIS_SERVICE_MATY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
