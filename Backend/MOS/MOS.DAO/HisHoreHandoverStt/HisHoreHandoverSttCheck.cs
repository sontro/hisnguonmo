using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisHoreHandoverStt
{
    partial class HisHoreHandoverSttCheck : EntityBase
    {
        public HisHoreHandoverSttCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_HORE_HANDOVER_STT>();
        }

        private BridgeDAO<HIS_HORE_HANDOVER_STT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
