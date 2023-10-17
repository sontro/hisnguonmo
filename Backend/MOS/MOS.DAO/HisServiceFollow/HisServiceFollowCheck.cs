using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisServiceFollow
{
    partial class HisServiceFollowCheck : EntityBase
    {
        public HisServiceFollowCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_FOLLOW>();
        }

        private BridgeDAO<HIS_SERVICE_FOLLOW> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
