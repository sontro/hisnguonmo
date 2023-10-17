using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisHoreHandover
{
    partial class HisHoreHandoverCheck : EntityBase
    {
        public HisHoreHandoverCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_HORE_HANDOVER>();
        }

        private BridgeDAO<HIS_HORE_HANDOVER> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
