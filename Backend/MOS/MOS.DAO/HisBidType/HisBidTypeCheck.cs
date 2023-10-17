using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisBidType
{
    partial class HisBidTypeCheck : EntityBase
    {
        public HisBidTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BID_TYPE>();
        }

        private BridgeDAO<HIS_BID_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
