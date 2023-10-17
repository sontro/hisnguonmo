using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisBid
{
    partial class HisBidCheck : EntityBase
    {
        public HisBidCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BID>();
        }

        private BridgeDAO<HIS_BID> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
