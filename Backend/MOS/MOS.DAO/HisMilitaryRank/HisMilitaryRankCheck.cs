using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMilitaryRank
{
    partial class HisMilitaryRankCheck : EntityBase
    {
        public HisMilitaryRankCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MILITARY_RANK>();
        }

        private BridgeDAO<HIS_MILITARY_RANK> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
