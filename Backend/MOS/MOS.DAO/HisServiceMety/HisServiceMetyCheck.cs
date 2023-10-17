using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisServiceMety
{
    partial class HisServiceMetyCheck : EntityBase
    {
        public HisServiceMetyCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_METY>();
        }

        private BridgeDAO<HIS_SERVICE_METY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
