using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMetyMety
{
    partial class HisMetyMetyCheck : EntityBase
    {
        public HisMetyMetyCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_METY_METY>();
        }

        private BridgeDAO<HIS_METY_METY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
