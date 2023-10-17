using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMetyMaty
{
    partial class HisMetyMatyCheck : EntityBase
    {
        public HisMetyMatyCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_METY_MATY>();
        }

        private BridgeDAO<HIS_METY_MATY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
