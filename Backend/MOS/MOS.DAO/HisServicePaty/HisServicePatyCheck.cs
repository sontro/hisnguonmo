using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisServicePaty
{
    partial class HisServicePatyCheck : EntityBase
    {
        public HisServicePatyCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_PATY>();
        }

        private BridgeDAO<HIS_SERVICE_PATY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
